using AutoMapper;
using FluentAssertions;
using Force.DeepCloner;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ZaminEducation.Data.DbContexts;
using ZaminEducation.Data.IRepositories;
using ZaminEducation.Data.Repositories;
using ZaminEducation.Domain.Entities.Commons;
using ZaminEducation.Domain.Entities.Courses;
using ZaminEducation.Domain.Entities.UserCourses;
using ZaminEducation.Domain.Entities.Users;
using ZaminEducation.Domain.Enums;
using ZaminEducation.Service.DTOs.Courses;
using ZaminEducation.Service.DTOs.Users;
using ZaminEducation.Service.Interfaces;
using ZaminEducation.Service.Interfaces.Courses;
using ZaminEducation.Service.Mappers;
using ZaminEducation.Service.Services;
using ZaminEducation.Service.Services.Courses;

namespace ZaminEducation.Test.Unit.Services.YouTube
{
    public partial class YoutubeServiceAndCourseServiceTest
    {
        private readonly ZaminEducationDbContext zaminEducationDbContext;

        private readonly IRepository<Course> courseRepositoryMock;
        private readonly IRepository<User> userRepositoryMock;
        private readonly IRepository<CourseCategory> courseCategoryRepositoryMock;
        private readonly IRepository<CourseVideo> courseVideoRepositoryMock;
        private readonly IRepository<CourseRate> courseRateRepositoryMock;
        private readonly IRepository<Attachment> attachmentReositoryMock;
        private readonly IRepository<CourseModule> courseModuleRepositoryMock;
        private readonly IRepository<ReferralLink> referralLinkRepositoryMock;

        private readonly ICourseCategoryService courseCategoryService;
        private readonly IYouTubeService youTubeService;
        private readonly IUserService userService;
        private readonly ICourseService courseService;
        private readonly IAttachmentService attachmentService;
        private readonly ICourseModuleService courseModuleService;
        private readonly IMapper mapper;

        public YoutubeServiceAndCourseServiceTest()
        {
            var options = new DbContextOptionsBuilder<ZaminEducationDbContext>()
                           .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            mapper = new MapperConfiguration(
                mapper => mapper.AddProfile<MappingProfile>()).CreateMapper();

            zaminEducationDbContext = new ZaminEducationDbContext(options);

            courseCategoryRepositoryMock = new Repository<CourseCategory>(zaminEducationDbContext);
            userRepositoryMock = new Repository<User>(zaminEducationDbContext);
            courseModuleRepositoryMock = new Repository<CourseModule>(zaminEducationDbContext);
            courseRepositoryMock = new Repository<Course>(zaminEducationDbContext);
            courseVideoRepositoryMock = new Repository<CourseVideo>(zaminEducationDbContext);
            attachmentReositoryMock = new Repository<Attachment>(zaminEducationDbContext);
            referralLinkRepositoryMock = new Repository<ReferralLink>(zaminEducationDbContext);
            courseRateRepositoryMock = new Repository<CourseRate>(zaminEducationDbContext);

            courseCategoryService = new CourseCategoryService(mapper, courseCategoryRepositoryMock);
            attachmentService = new AttachmentService(attachmentReositoryMock);
            userService = new UserService(userRepositoryMock, mapper, attachmentService);
            courseModuleService = new CourseModuleService(courseModuleRepositoryMock, courseService, mapper);
            youTubeService = new YouTubeService(courseVideoRepositoryMock, courseRepositoryMock);
            courseService = new CourseService(courseRepositoryMock,
                youTubeService,
                courseRateRepositoryMock,
                attachmentService,
                userService,
                mapper,
                referralLinkRepositoryMock);
        }

        private CourseForCreationDto CreateRandomCourse(CourseForCreationDto courseForCreationDto)
        {
            courseForCreationDto.YouTubePlaylistLink = "https://www.youtube.com/watch?v=2NwXtLaZJkQ&list=PLAXSS6gGBPcVrqEaXc1Av4CLaLxbv9HOx";
            courseForCreationDto.Description = Faker.Lorem.Sentence(3);
            courseForCreationDto.Name = Faker.Name.First();
            courseForCreationDto.Level = (CourseLevel)Faker.RandomNumber.Next(0, 2);
            courseForCreationDto.Description = Faker.Lorem.Sentence(5);

            return courseForCreationDto;
        }

        private CourseCategoryForCreationDto CreateRandomCategory(CourseCategoryForCreationDto courseCategoryForCreationDto)
        {
            courseCategoryForCreationDto.Name = Faker.Lorem.Sentence(2);
            return courseCategoryForCreationDto;
        }
        private UserForCreationDto CreateRandomAuthor(UserForCreationDto userForCreationDto)
        {
            userForCreationDto.FirstName = Faker.Name.First();
            userForCreationDto.LastName = Faker.Name.Last();
            userForCreationDto.Username = Faker.Name.Middle();
            userForCreationDto.AddressId = null;
            userForCreationDto.Gender = (Gender)Faker.RandomNumber.Next(0, 1);
            userForCreationDto.Bio = Faker.Lorem.Sentence(1);
            userForCreationDto.DateOfBirth = DateTime.UtcNow;
            userForCreationDto.Password = Faker.Lorem.Sentence(1);



            return userForCreationDto;
        }

        private async ValueTask<(string YoutubePlaylistLink, long CourseId, long CourseModuleId)> CreateAllDependencies()
        {
            var randomAuthor = CreateRandomAuthor(new UserForCreationDto());
            var randomCategory = CreateRandomCategory(new CourseCategoryForCreationDto());
            var randomCourse = CreateRandomCourse(new CourseForCreationDto());

            var inputAuthor = randomAuthor;
            var inputCategory = randomCategory;
            var inputCourse = randomCourse;

            var expectedAuthor = inputAuthor.DeepClone();
            var expectedCategory = inputCategory.DeepClone();
            var expectedCourse = inputCourse.DeepClone();

            // when
            var actualAuthor = await userService.CreateAsync(inputAuthor);
            await userService.ChangeRoleAsync(actualAuthor.Id, 2);

            var actualCategory = await courseCategoryService.CreateAsync(inputCategory);


            inputCourse.AuthorId = actualAuthor.Id;
            inputCourse.CategoryId = actualCategory.Id;

            var actualCourse = await courseService.CreateAsync(inputCourse);


            var actualCourseModuleId = (await courseService.GetAsync(c => c.Id == actualCourse.Id)).Modules.FirstOrDefault().Id;

            // then
            actualCourse.Should().NotBeNull();
            actualCourse.Name.Should().BeEquivalentTo(expectedCourse.Name);

            actualCategory.Should().NotBeNull();
            actualCategory.Name.Should().BeEquivalentTo(expectedCategory.Name);

            actualAuthor.Should().NotBeNull();
            actualAuthor.Username.Should().BeEquivalentTo(expectedAuthor.Username);

            return (actualCourse.YouTubePlaylistLink, actualCourse.Id, actualCourseModuleId);
        }
    }
}

