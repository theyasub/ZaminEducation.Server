using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ZaminEducation.Data.IRepositories;
using ZaminEducation.Domain.Configurations;
using ZaminEducation.Domain.Entities.Courses;
using ZaminEducation.Service.Exceptions;
using ZaminEducation.Service.Extensions;
using ZaminEducation.Service.Interfaces;

namespace ZaminEducation.Service.Services
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> courseRepository;

        public CourseService(IRepository<Course> courserRepository)
        {
            this.courseRepository = courserRepository;
        }

        public async Task<IEnumerable<CourseModule>> GetCourseModulesAsync(Expression<Func<Course, bool>> expression)
        {
            var course = await courseRepository.GetAsync(expression, new string[] { "Modules" });

            if (course is null)
                throw new ZaminEducationException(404, "Course not found");

            return course.Modules;
        }

        public async Task<IEnumerable<CourseTarget>> GetCourseTargetsAsync(Expression<Func<Course, bool>> expression)
        {
            var course = await courseRepository.GetAsync(expression, new string[] { "Targets" });

            if (course is null)
                throw new ZaminEducationException(404, "Course not found");

            return course.Targets;
        }

        public async Task<IEnumerable<CourseVideo>> GetCourseVideosAsync(Expression<Func<Course, bool>> expression)
        {
            var course = await courseRepository.GetAsync(expression, new string[] { "Videos" });

            if (course is null)
                throw new ZaminEducationException(404, "Course not found");

            return course.Videos;
        }

        public async ValueTask<IEnumerable<Course>> GetAllAsync(PaginationParams @params,
           string search)
               => await courseRepository.GetAll(
                   c => c.Id.ToString() == search ||
                   c.Name == search)?
                       .ToPagedList(@params).ToListAsync();
    }
}