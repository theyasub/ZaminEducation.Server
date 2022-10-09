
using AutoMapper;
using System.Linq.Expressions;
using ZaminEducation.Data.IRepositories;
using ZaminEducation.Domain.Entities.Courses;
using ZaminEducation.Service.DTOs.Courses;
using ZaminEducation.Service.Exceptions;
using ZaminEducation.Service.Extensions;
using ZaminEducation.Service.Interfaces;

namespace ZaminEducation.Service.Services
{
    public class CourseModuleService : ICourseModuleService
    {
        private readonly IRepository<CourseModule> courseModuleRepository;
        private readonly IMapper mapper;
        public CourseModuleService(IRepository<CourseModule> courseModuleRepository, IMapper mapper)
        {
            this.courseModuleRepository = courseModuleRepository;
            this.mapper = mapper;
        }

        public async ValueTask<CourseModule> CreateAsync(CourseModuleForCreationDto dto)
        {
            CourseModule mapped = this.mapper.Map<CourseModule>(dto);
            mapped.Create();

            CourseModule entity = await this.courseModuleRepository.AddAsync(mapped);

            await this.courseModuleRepository.SaveChangesAsync();

            return entity;
        }

        public async ValueTask<bool> DeleteAsync(Expression<Func<CourseModule, bool>> expression)
        {
            CourseModule courseModule = await this.courseModuleRepository.GetAsync(expression);

            if (courseModule is null)
                throw new ZaminEducationException(404, "Course module not found");

            this.courseModuleRepository.Delete(courseModule);

            await this.courseModuleRepository.SaveChangesAsync();

            return true;
        }

        public async ValueTask<CourseModule> UpdateAsync(Expression<Func<CourseModule, bool>> expression, CourseModuleForCreationDto dto)
        {
            CourseModule courseModule = await this.courseModuleRepository.GetAsync(expression);

            if (courseModule is null)
                throw new ZaminEducationException(404, "Course module not found");

            courseModule = this.mapper.Map(dto, courseModule);
            courseModule.Update();

            CourseModule entity = this.courseModuleRepository.Update(courseModule);

            await this.courseModuleRepository.SaveChangesAsync();

            return entity;
        }
    }
}
