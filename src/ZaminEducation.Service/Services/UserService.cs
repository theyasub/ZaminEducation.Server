using AutoMapper;
using System.Linq.Expressions;
using ZaminEducation.Data.IRepositories;
using ZaminEducation.Domain.Entities.Users;
using ZaminEducation.Domain.Enums;
using ZaminEducation.Service.DTOs.Commons;
using ZaminEducation.Service.DTOs.Users;
using ZaminEducation.Service.Exceptions;
using ZaminEducation.Service.Extensions;
using ZaminEducation.Service.Interfaces;

namespace ZaminEducation.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IMapper mapper;
        private readonly IAttachmentService attachmentService;

        public UserService(IRepository<User> userRepository, IMapper mapper, IAttachmentService attachmentService)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.attachmentService = attachmentService;
        }

        public async ValueTask<User> CreateAsync(UserForCreationDto dto)
        {
            var user = await userRepository.GetAsync(u => u.Username == dto.Username
                                                            && u.State != ItemState.Deleted);

            if (user is not null)
                throw new ZaminEducationException(400, "User already exists");

            User mappedUser = mapper.Map<User>(dto);

            mappedUser.Password = dto.Password.Encrypt();
            mappedUser.Create();

            User newUser = await userRepository.AddAsync(mappedUser);

            await userRepository.SaveChangesAsync();

            return newUser;
        }

        public async ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var user = await userRepository.GetAsync(expression);

            if (user is null || user.State == ItemState.Deleted)
                throw new ZaminEducationException(404, "User not found");

            userRepository.Delete(user);

            await userRepository.SaveChangesAsync();

            return true;
        }

        public async ValueTask<User> GetAsync(Expression<Func<User, bool>> expression)
        {
            var user = await userRepository.GetAsync(expression, new string[] { "Address", "Image" });

            if (user is null)
                throw new ZaminEducationException(404, "User not found");

            return user;
        }

        public async ValueTask<User> UpdateAsync(long id, UserForUpdateDto dto)
        {
            var user = await userRepository.GetAsync(u => u.Id == id && u.State != ItemState.Deleted);

            if (user is null)
                throw new ZaminEducationException(404, "User not found!");

            var alredyExistsUser = await userRepository.GetAsync(u => u.Username == dto.Username
                                                                    && u.State != ItemState.Deleted && u.Id != id);
            if (alredyExistsUser is not null)
                throw new ZaminEducationException(400, "Login or Password is incorrect!");

            user = mapper.Map(dto, user);

            user.Password = user.Password.Encrypt();
            user.Update();

            userRepository.Update(user);

            await userRepository.SaveChangesAsync();

            return user;
        }

        public async ValueTask<User> AddAttachmentAsync(long userId, AttachmentForCreationDto attachmentForCreationDto)
        {
            var attachment = await attachmentService.UploadAsync(attachmentForCreationDto);

            var user = await userRepository.GetAsync(u => u.Id == userId);

            if (user == null)
                throw new ZaminEducationException(404, "User not found");

            user.ImageId = attachment.Id;

            userRepository.Update(user);
            await userRepository.SaveChangesAsync();

            return user;
        }

        public async ValueTask<User> ChangeRoleAsync(long userId, byte roleId)
        {
            var account = await userRepository.GetAsync(u => u.Id == userId
                                    && u.State != ItemState.Deleted && u.Role != UserRole.SuperAdmin);

            if (account is null)
                throw new ZaminEducationException(404, "User not found");

            if (roleId != 1 && roleId != 2)
                throw new ZaminEducationException(404, "Such role does not exist");

            account.Role = (UserRole)roleId;
            account.Update();

            await userRepository.SaveChangesAsync();

            return account;
        }
    }
}
