using System.Linq.Expressions;
using ZaminEducation.Domain.Entities.Users;
using ZaminEducation.Service.DTOs.Commons;
using ZaminEducation.Service.DTOs.Users;

namespace ZaminEducation.Service.Interfaces
{
    public interface IUserService
    {
        ValueTask<User> CreateAsync(UserForCreationDto dto);
        ValueTask<User> UpdateAsync(long id, UserForUpdateDto dto);
        ValueTask<User> GetAsync(Expression<Func<User, bool>> expression);
        ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression);
        ValueTask<User> AddAttachmentAsync(long id, AttachmentForCreationDto attachmentForCreationDto);
        ValueTask<User> ChangeRoleAsync(long userId, byte roleId);
    }
}
