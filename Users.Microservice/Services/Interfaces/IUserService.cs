using System.Linq.Expressions;
using Users.Microservice.Models.Configurations;
using Users.Microservice.Models.Entities;
using Users.Microservice.Services.DTOs;

namespace Users.Microservice.Services.Interfaces
{
    public interface IUserService
    {
        ValueTask<User> CreateAsync(UserForCreationDto dto);
        ValueTask<User> UpdateAsync(long id, UserForUpdateDto dto);
        ValueTask<User> GetAsync(Expression<Func<User, bool>> expression);
        ValueTask<IEnumerable<User>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null, string search = null);
        ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression);
        ValueTask<User> GetInfoAsync();
        ValueTask<User> ChangePasswordAsync(UserForChangePassword dto);
        ValueTask<User> AddAttachmentAsync(long id, AttachmentForCreationDto attachmentForCreationDto);
        ValueTask<User> ChangeRoleAsync(long userId, byte roleId);
    }
}
