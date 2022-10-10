using System.Linq.Expressions;
using ZaminEducation.Domain.Configurations;
using ZaminEducation.Domain.Entities.user;
using ZaminEducation.Service.DTOs.Users;

namespace ZaminEducation.Service.Interfaces
{
    public interface IZCApplicantDirectionService
    {
        ValueTask<ZCApplicantDirection> CreateAsync(ZCApplicantDirectionForCreationDto dto);
        ValueTask<ZCApplicantDirection> UpdateAsync(Expression<Func<ZCApplicantDirection, bool>> expression,
            ZCApplicantDirectionForCreationDto dto);
        ValueTask<bool> DeleteAsync(Expression<Func<ZCApplicantDirection, bool>> expression);
        ValueTask<ZCApplicantDirection> GetAsync(Expression<Func<ZCApplicantDirection, bool>> expression);
        ValueTask<IEnumerable<ZCApplicantDirection>> GetAllAsync(PaginationParams @params);
    }
}
