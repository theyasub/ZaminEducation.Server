using System.Linq.Expressions;
using ZaminEducation.Domain.Configurations;
using ZaminEducation.Domain.Entities.Users;
using ZaminEducation.Service.DTOs.Commons;
using ZaminEducation.Service.DTOs.Users;

namespace ZaminEducation.Service.Interfaces
{
    public interface IUserService
    {
        ValueTask<User> CreateAsync(UserForCreationDto dto);
        ValueTask<User> UpdateAsync(long id, UserForCreationDto dto);
        ValueTask<User> GetAsync(Expression<Func<User, bool>> expression);
        ValueTask<IEnumerable<User>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null);
        ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression);
        ValueTask<User> GetInfoAsync();
        ValueTask<User> ChangePasswordAsync(UserForChangePassword dto);
        ValueTask<User> AddAttachmentAsync(long id, AttachmentForCreationDto attachmentForCreationDto);

    }
}
