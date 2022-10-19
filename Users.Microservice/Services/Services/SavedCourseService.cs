using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Users.Microservice.Data.IRepositories;
using Users.Microservice.Models.Configurations;
using Users.Microservice.Models.Entities;
using Users.Microservice.Services.DTOs;
using Users.Microservice.Services.Exceptions;
using Users.Microservice.Services.Extentions;
using Users.Microservice.Services.Helpers;
using Users.Microservice.Services.Interfaces;

namespace Users.Microservice.Services.Services
{
    public class SavedCoursesService : ISavedCourseService
    {
        private readonly IRepository<SavedCourse> savedCourseRepository;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Course> courseRepository;

        public SavedCoursesService(IRepository<SavedCourse> savedCourseRepository, IRepository<User> userRepository, IRepository<Course> courseRepository)
        {
            this.savedCourseRepository = savedCourseRepository;
            this.courseRepository = courseRepository;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Agar course save qilingan bo'lsa SavedCourse dan o'chirib tashlab false qaytaradi 
        /// save qilinmagan bo'lsa save qilib true qaytaradi.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async ValueTask<bool> ToggleAsync(SavedCourseForCreationDto dto)
        {
            var existsUser = await userRepository.GetAsync(x => x.Id == dto.UserId);
            var existsCourse = await courseRepository.GetAsync(c => c.Id == dto.CourseId);

            if (existsCourse is null)
                throw new UserMicroserviceException(404, "Course not found");
            if (existsUser is null)
                throw new UserMicroserviceException(404, "User not found");
            if (dto.UserId != HttpContextHelper.UserId)
                throw new UserMicroserviceException(403, "Forbidden");


            var result = await savedCourseRepository.GetAsync(p => p.CourseId == dto.CourseId
                && p.UserId == dto.UserId);

            if (result is not null)
            {
                savedCourseRepository.Delete(result);
                await savedCourseRepository.SaveChangesAsync();
                return false;
            }
            var newSavedCourse = new SavedCourse()
            {
                CourseId = dto.CourseId,
                UserId = dto.UserId
            };
            newSavedCourse.Create();
            newSavedCourse = await savedCourseRepository.AddAsync(newSavedCourse);
            await savedCourseRepository.SaveChangesAsync();

            return true;
        }

        public async ValueTask<IEnumerable<SavedCourse>> GetAllAsync(PaginationParams @params, Expression<Func<SavedCourse, bool>> expression = null, string search = null)
        {
            var pagedList = savedCourseRepository.GetAll(expression, new string[] { "Course", "User" }, false).Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize);

            return !string.IsNullOrEmpty(search)
                ? await pagedList.Where(sc => (sc.Course.Name == search ||
                    sc.Course.Category.Name == search) &&
                    sc.UserId.Equals(HttpContextHelper.UserId)).ToListAsync()
                : await pagedList.Where(c => c.UserId.Equals(HttpContextHelper.UserId)).ToListAsync();
        }

        public async ValueTask<SavedCourse> GetAsync(Expression<Func<SavedCourse, bool>> expression = null)
        {
            var result = await savedCourseRepository.GetAsync(expression, new string[] { "Course", "User" });

            if (result is null)
                throw new UserMicroserviceException(404, "Saved course not found");

            return result;
        }
    }
}
