
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ZaminEducation.Data.IRepositories;
using ZaminEducation.Domain.Entities.Courses;
using ZaminEducation.Service.DTOs.Courses;
using ZaminEducation.Service.Exceptions;
using ZaminEducation.Service.Extensions;
using ZaminEducation.Service.Interfaces;
using ZaminEducation.Service.Interfaces.Courses;

namespace ZaminEducation.Service.Services
{
    public class CourseModuleService : ICourseModuleService
    {
        private readonly IRepository<CourseModule> courseModuleRepository;
        private readonly ICourseService courseService;
        private readonly IMapper mapper;
        public CourseModuleService(
            IRepository<CourseModule> courseModuleRepository,
            ICourseService courseService,
            IMapper mapper)
        {
            this.courseModuleRepository = courseModuleRepository;
            this.courseService = courseService;
            this.mapper = mapper;
        }

        public async ValueTask<CourseModule> CreateAsync(CourseModuleForCreationDto dto)
        {
            // Check if the module name has not been used before
            await this.Unused(dto);

            CourseModule mapped = this.mapper.Map<CourseModule>(dto);
            mapped.Create();

            CourseModule entity = await this.courseModuleRepository.AddAsync(mapped);

            await this.courseModuleRepository.SaveChangesAsync();

            return entity;
        }

        public async Task<ICollection<CourseModule>> CreateRangeAsync(long courseId, IEnumerable<string> moduleNames)
        {
            IEnumerable<string> entityModuleNames = (await this.GetAllAsync(courseId, cm => cm.CourseId.Equals(courseId)))
                                                               .Select(cm => cm.Name);
            foreach (string moduleName in moduleNames)
            {
                if (entityModuleNames.Contains(moduleName))
                    throw new ZaminEducationException(400, $"This '{moduleName}' is already used");
            }

            ICollection<CourseModule> courseModules = new List<CourseModule>();

            foreach (var name in moduleNames)
            {
                CourseModule courseModule = new CourseModule()
                {
                    Name = name,
                    CourseId = courseId,
                };

                courseModule.Create();

                CourseModule entity = await this.courseModuleRepository.AddAsync(courseModule);

                courseModules.Add(entity);
            }

            await this.courseModuleRepository.SaveChangesAsync();

            return courseModules;
        }

        public async ValueTask<IEnumerable<CourseModule>> GetAllAsync(long courseId, Expression<Func<CourseModule, bool>> expression)
        {
            await this.courseService.RetrieveAsync(c => c.Id.Equals(courseId));

            return await this.courseModuleRepository.GetAll(expression, new string[] { "Modules" }, false).ToListAsync();
        }

        public async ValueTask<bool> DeleteAsync(Expression<Func<CourseModule, bool>> expression)
        {
            CourseModule courseModule = await this.courseModuleRepository.GetAsync(expression);

            if (courseModule is null)
                throw new ZaminEducationException(404, "Module not found");

            this.courseModuleRepository.Delete(courseModule);

            await this.courseModuleRepository.SaveChangesAsync();

            return true;
        }

        public async ValueTask<CourseModule> UpdateAsync(Expression<Func<CourseModule, bool>> expression, CourseModuleForCreationDto dto)
        {
            CourseModule courseModule = await this.courseModuleRepository.GetAsync(expression);

            if (courseModule is null)
                throw new ZaminEducationException(404, "Module not found");

            // Check if the module name has not been used before
            await this.Unused(dto);

            courseModule = this.mapper.Map(dto, courseModule);
            courseModule.Update();

            CourseModule entity = this.courseModuleRepository.Update(courseModule);

            await this.courseModuleRepository.SaveChangesAsync();
            return entity;
        }

        private async ValueTask Unused(CourseModuleForCreationDto dto)
        {
            CourseModule module = await this.courseModuleRepository.GetAsync(cm => cm.CourseId.Equals(dto.CourseId) &&
                                                                                   cm.Name.Equals(dto.Name));
            if (module is not null)
                throw new ZaminEducationException(400, $"'{dto.Name}' is already used");
        }
    }
}
