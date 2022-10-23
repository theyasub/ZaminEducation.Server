using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Users.Microservice.Data.IRepositories;
using Users.Microservice.Models.Configurations;
using Users.Microservice.Models.Entities;
using Users.Microservice.Models.Enums;
using Users.Microservice.Services.DTOs;
using Users.Microservice.Services.Exceptions;
using Users.Microservice.Services.Extentions;
using Users.Microservice.Services.Helpers;
using Users.Microservice.Services.Interfaces;

namespace Users.Microservice.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IAttachmentService attachmentService;

        public UserService(IRepository<User> userRepository, IAttachmentService attachmentService)
        {
            this.userRepository = userRepository;
            this.attachmentService = attachmentService;
        }

        public async ValueTask<User> CreateAsync(UserForCreationDto dto)
        {
            var user = await userRepository.GetAsync(u => u.Username == dto.Username
                                                            && u.State != ItemState.Deleted);

            if (user is not null)
                throw new UserMicroserviceException(400, "User already exists");

            User mappedUser = new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username,
                Password = dto.Password.Encrypt(),
                Bio = dto.Bio,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                Role = UserRole.SuperAdmin
            };
            mappedUser.Create();

            mappedUser = await userRepository.AddAsync(mappedUser);
            await userRepository.SaveChangesAsync();

            return mappedUser;
        }

        public async ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var user = await userRepository.GetAsync(expression);

            if (user is null || user.State == ItemState.Deleted)
                throw new UserMicroserviceException(404, "User not found");

            userRepository.Delete(user);

            await userRepository.SaveChangesAsync();

            return true;
        }

        public async ValueTask<IEnumerable<User>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null, string search = null)
        {
            //var users = userRepository.GetAll(expression, new string[] { "Address", "Image" }, isTracking: false);
            var users = userRepository.GetAll(expression, isTracking: false);

            return !string.IsNullOrEmpty(search)
                ? await users.Where(u => u.FirstName == search ||
                        u.LastName == search ||
                        u.Username == search ||
                        u.Bio.Contains(search)).Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize).ToListAsync()
                : (IEnumerable<User>)await users.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize).ToListAsync();
        }

        public async ValueTask<User> GetAsync(Expression<Func<User, bool>> expression)
        {
            //var user = await userRepository.GetAsync(expression, new string[] { "Address", "Image" });
            var user = await userRepository.GetAsync(expression);
            if (user is null)
                throw new UserMicroserviceException(404, "User not found");

            return user;
        }

        public async ValueTask<User> UpdateAsync(long id, UserForUpdateDto dto)
        {
            var user = await userRepository.GetAsync(u => u.Id == id && u.State != ItemState.Deleted);

            if (user is null)
                throw new UserMicroserviceException(404, "User not found!");

            var alredyExistsUser = await userRepository.GetAsync(u => u.Username == dto.Username
                                                                    && u.State != ItemState.Deleted && u.Id != id);
            if (alredyExistsUser is not null)
                throw new UserMicroserviceException(400, "Login or Password is incorrect!");

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Username = dto.Username;
            user.Bio = dto.Bio;
            user.Gender = dto.Gender;
            user.Password = user.Password.Encrypt();
            user.Update();

            userRepository.Update(user);
            await userRepository.SaveChangesAsync();

            return user;
        }

        public async ValueTask<User> GetInfoAsync(long id)
        {
            return await userRepository.GetAsync(u => u.Id == id);
        }
        public async ValueTask<User> AddAttachmentAsync(long userId, AttachmentForCreationDto attachmentForCreationDto)
        {
            var attachment = await attachmentService.UploadAsync(attachmentForCreationDto);

            var user = await userRepository.GetAsync(u => u.Id == userId);

            if (user == null)
                throw new UserMicroserviceException(404, "User not found");

            user.ImageId = attachment.Id;

            userRepository.Update(user);
            await userRepository.SaveChangesAsync();

            return user;
        }

        public async ValueTask<User> ChangePasswordAsync(UserForChangePassword dto)
        {
            User existUser = await userRepository.GetAsync(user => user.Username == dto.Username);

            if (existUser is null)
                throw new Exception("This Username does not exist");

            else if (dto.NewPassword != dto.ComfirmPassword)
                throw new Exception("New password and comfirm password are not equal");

            else if (existUser.Password != dto.OldPassword.Encrypt())
                throw new Exception("Password is incorrect!");

            existUser.Password = dto.NewPassword.Encrypt();

            existUser = userRepository.Update(existUser);
            await userRepository.SaveChangesAsync();

            return existUser;
        }

        public async ValueTask<User> ChangeRoleAsync(long userId, UserRole role)
        {
            var account = await userRepository.GetAsync(u => u.Id == userId
                                    && u.State != ItemState.Deleted && u.Role != UserRole.SuperAdmin);

            if (account is null)
                throw new UserMicroserviceException(404, "User not found");

            if (role != UserRole.Admin && role != UserRole.Mentor)
                throw new UserMicroserviceException(404, "Such role does not exist");

            account.Role = role;
            account.Update();

            await userRepository.SaveChangesAsync();

            return account;
        }
    }
}
