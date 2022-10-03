using System.Linq.Expressions;
using ZaminEducation.Domain.Configurations;
using ZaminEducation.Domain.Entities.Commons;
using ZaminEducation.Domain.Entities.UserCourses;
using ZaminEducation.Service.DTOs.UserCourses;

namespace ZaminEducation.Service.Interfaces
{
    public interface ICertificateService
    {
        public ValueTask<Attachment> CreateAsync(CertificateForCreationDto certificateForCreation);
        public ValueTask<IEnumerable<Certificate>> GetAllAsync(PaginationParams @params,
            Expression<Func<Certificate, bool>> expression = null);
        public ValueTask<Certificate> GetAsync(Expression<Func<Certificate, bool>> expression);
    }
}
