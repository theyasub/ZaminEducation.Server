using Users.Microservice.Data.IRepositories;
using Users.Microservice.Data.Repositories;
using Users.Microservice.Models.Entities;
using Users.Microservice.Services.Interfaces;
using Users.Microservice.Services.Services;

namespace Users.Microservice.Extentions
{
    public static class ServiceExtentions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Course>, Repository<Course>>();
            services.AddScoped<IRepository<SavedCourse>, Repository<SavedCourse>>();
            services.AddScoped<IRepository<Attachment>, Repository<Attachment>>();

            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISavedCourseService, SavedCoursesService>();
        }
    }
}
