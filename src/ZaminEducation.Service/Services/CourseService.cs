using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ZaminEducation.Data.IRepositories;
using ZaminEducation.Domain.Configurations;
using ZaminEducation.Domain.Entities.Courses;
using ZaminEducation.Domain.Entities.UserCourses;
using ZaminEducation.Service.DTOs.Courses;
using ZaminEducation.Service.Exceptions;
using ZaminEducation.Service.Extensions;
using ZaminEducation.Service.Helpers;
using ZaminEducation.Service.Interfaces;
using ZaminEducation.Service.Interfaces.Courses;
using ZaminEducation.Service.ViewModels;

namespace ZaminEducation.Service.Services.Courses
{
    public class CourseService : ICourseService
    {
        private readonly IYouTubeService youTubeService;
        private readonly IRepository<Course> courseRepository;
        private readonly IRepository<CourseRate> courseRateRepository;
        private readonly IRepository<ReferralLink> referralLinkRepository;
        private readonly IAttachmentService attachmentService;
        private readonly IMapper mapper;

        public CourseService(
            IRepository<Course> courseRepository,
            IYouTubeService youTubeService,
            IRepository<CourseRate> courseRateRepository,
            IAttachmentService attachmentService,
            IMapper mapper, IRepository<ReferralLink> referralLinkRepository)
        {
            this.courseRepository = courseRepository;
            this.youTubeService = youTubeService;
            this.courseRateRepository = courseRateRepository;
            this.attachmentService = attachmentService;
            this.mapper = mapper;
            this.referralLinkRepository = referralLinkRepository;
        }

        public async ValueTask<Course> CreateAsync(CourseForCreationDto courseForCreationDto)
        {
            Course course = await courseRepository.GetAsync(c =>
                c.YouTubePlaylistLink.Equals(courseForCreationDto.YouTubePlaylistLink));

            if (course is not null)
                throw new ZaminEducationException(400, "Course already exists");

            var attachmentDto = courseForCreationDto.Image.ToAttachmentOrDefault();
            var attachment = await this.attachmentService.UploadAsync(dto: attachmentDto);

            Course mappedCourse = mapper.Map<Course>(source: courseForCreationDto);

            mappedCourse.ImageId = attachment.Id;
            mappedCourse.Create();
            Course entity = await courseRepository.AddAsync(entity: mappedCourse);

            await courseRepository.SaveChangesAsync();

            entity.Videos = (ICollection<CourseVideo>)await youTubeService.CreateRangeAsync(
                youtubePlaylist: courseForCreationDto.YouTubePlaylistLink,
                courseId: entity.Id);

            return entity;
        }

        public async ValueTask<string> GenerateLinkAsync(long courseId)
        {
            var linkExists = await referralLinkRepository.GetAsync(
                l => l.CourseId == courseId &&
                l.UserId == (long)HttpContextHelper.UserId && l.State != Domain.Enums.ItemState.Deleted);


            if (linkExists is null)
            {
                string generatedLink = string.Concat(Guid.NewGuid().ToString());

                ReferralLink link = new ReferralLink()
                {
                    CourseId = courseId,
                    UserId = (long)HttpContextHelper.UserId,
                    GeneratedLink = generatedLink
                };

                await referralLinkRepository.AddAsync(link);
                await referralLinkRepository.SaveChangesAsync();

                return generatedLink;
            }

            return linkExists.GeneratedLink;
        }

        public async ValueTask<bool> DeleteAsync(Expression<Func<Course, bool>> expression)
        {
            Course course = await courseRepository.GetAsync(expression: expression);

            if (course is null)
                throw new ZaminEducationException(404, "Course not found");

            courseRepository.Delete(entity: course);

            await courseRepository.SaveChangesAsync();

            return true;
        }

        public async ValueTask<IEnumerable<Course>> GetAllAsync(
            PaginationParams @params,
            Expression<Func<Course, bool>> expression = null,
            string search = null)
        {
            IQueryable<Course> pagedList = courseRepository.GetAll(
                expression: expression,
                includes: new string[] { "Author", "Category", "Image", "Rates" },
                isTracking: false)
                .ToPagedList(@params);

            return !string.IsNullOrEmpty(search)
                ? pagedList.Where(
                    c => c.Name == search ||
                    c.Author.FirstName == search ||
                    c.Author.LastName == search ||
                    c.Author.Username == search ||
                    c.Description.Contains(search) ||
                    c.Category.Name == search)
                : await pagedList.ToListAsync();
        }

        public async ValueTask<CourseViewModel> GetAsync(Expression<Func<Course, bool>> expression)
        {
            Course course = await courseRepository.GetAsync(
                expression: expression,
                includes: new string[] { "Category", "Image", "Modules", "Targets", "Rates", "Videos" });

            if (course is null)
                throw new ZaminEducationException(404, "Course not found");

            course.ViewCount++;

            CourseViewModel courseView = mapper.Map<CourseViewModel>(source: course);

            courseView.Rate = CalculateRates(rates: course.Rates);

            await courseRepository.SaveChangesAsync();

            return courseView;
        }

        public async ValueTask<Course> UpdateAsync(
            Expression<Func<Course, bool>> expression,
            CourseForCreationDto courseForCreationDto)
        {
            Course course = await courseRepository.GetAsync(expression: expression);

            if (course is null)
                throw new ZaminEducationException(404, "Course not found");

            var attachmentDto = courseForCreationDto.Image.ToAttachmentOrDefault();
            var attachment = await this.attachmentService.UploadAsync(dto: attachmentDto);

            course = mapper.Map(courseForCreationDto, course);

            course.ImageId = attachment.Id;
            course.Update();

            course = courseRepository.Update(entity: course);

            await courseRepository.SaveChangesAsync();

            return course;
        }

        public async ValueTask<IEnumerable<CourseModule>> GetCourseModulesAsync(Expression<Func<Course, bool>> expression)
        {
            var course = await courseRepository.GetAsync(expression, new string[] { "Modules" });

            if (course is null)
                throw new ZaminEducationException(404, "Course not found");

            return course.Modules;
        }

        public async ValueTask<IEnumerable<CourseTarget>> GetCourseTargetsAsync(Expression<Func<Course, bool>> expression)
        {
            var course = await courseRepository.GetAsync(expression, new string[] { "Targets" });

            if (course is null)
                throw new ZaminEducationException(404, "Course not found");

            return course.Targets;
        }

        public async ValueTask<IEnumerable<CourseVideo>> GetCourseVideosAsync(Expression<Func<Course, bool>> expression)
        {
            var course = await courseRepository.GetAsync(expression, new string[] { "Videos" });

            if (course is null)
                throw new ZaminEducationException(404, "Course not found");

            return course.Videos;
        }



        private double CalculateRates(IEnumerable<CourseRate> rates)
            => (double)rates.Sum(r => r.Value) / (double)rates.Count();

        public async Task<CourseRate> Rate(long id, byte value)
        {
            var course = await courseRepository.GetAsync(c => c.Id == id);

            if (course is null)
                throw new ZaminEducationException(404, "Course not found");

            var courseRate = await courseRateRepository.GetAsync(
                                cr => cr.UserId == HttpContextHelper.UserId &&
                                      cr.CourseId == id);

            if (courseRate is not null)
            {
                courseRate.Value = value;
                courseRate.Update();

                await courseRateRepository.SaveChangesAsync();

                return courseRate;
            }

            var newCourseRate = await courseRateRepository.AddAsync(new CourseRate()
            {
                Value = value,
                CourseId = id,
                UserId = (long)HttpContextHelper.UserId,
                CreatedBy = HttpContextHelper.UserId
            });
            await courseRateRepository.SaveChangesAsync();

            return newCourseRate;
        }

        public async Task<CourseRate> GetCourseRateOfUser(long id)
        {
            var course = await courseRepository.GetAsync(c => c.Id == id);

            if (course is null)
                throw new ZaminEducationException(404, "Course not found");

            var existCourseRate = await courseRateRepository.GetAsync(
                                    cr => cr.UserId == HttpContextHelper.UserId &&
                                           cr.CourseId == id);

            if (existCourseRate is null)
                throw new ZaminEducationException(404, "CourseRate not found");

            return existCourseRate;
        }
    }
}