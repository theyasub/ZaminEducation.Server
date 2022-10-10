using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using ZaminEducation.Data.DbContexts;
using ZaminEducation.Data.IRepositories;
using ZaminEducation.Data.Repositories;
using ZaminEducation.Domain.Entities.Commons;
using ZaminEducation.Domain.Entities.Users;
using ZaminEducation.Domain.Enums;
using ZaminEducation.Service.DTOs.Commons;
using ZaminEducation.Service.DTOs.Users;
using ZaminEducation.Service.Helpers;
using ZaminEducation.Service.Interfaces;
using ZaminEducation.Service.Mappers;
using ZaminEducation.Service.Services;

namespace ZaminEducation.Test.Unit.Services.Users
{
    public partial class UserServiceTest
    {
        private readonly IRepository<User> userRepositoryMock;
        private readonly IRepository<Attachment> attachmentRepositoryMock;
        private readonly IMapper mapper;
        private readonly ZaminEducationDbContext zaminEducationDbContext;
        private readonly IUserService userService;
        private readonly IAttachmentService attachmentService;


        public UserServiceTest()
        {
            var options = new DbContextOptionsBuilder<ZaminEducationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            zaminEducationDbContext = new ZaminEducationDbContext(options);
            userRepositoryMock = new Repository<User>(zaminEducationDbContext);
            attachmentRepositoryMock = new Repository<Attachment>(zaminEducationDbContext);

            mapper = new MapperConfiguration(m =>
            {
                m.AddProfile<MappingProfile>();
            }).CreateMapper();

            attachmentService = new AttachmentService(attachmentRepositoryMock);
            userService = new UserService(userRepositoryMock, mapper, attachmentService);

        }

        private UserForCreationDto CreateRandomUser(UserForCreationDto userForCreationDto)
        {
            userForCreationDto.FirstName = Faker.Name.First();
            userForCreationDto.LastName = Faker.Name.Last();
            userForCreationDto.Username = Faker.Name.Middle();
            userForCreationDto.AddressId = null;
            userForCreationDto.Gender = (Gender)Faker.RandomNumber.Next(0, 1);
            userForCreationDto.Bio = Faker.Lorem.Sentence(1);
            userForCreationDto.DateOfBirth = DateTime.UtcNow;
            userForCreationDto.Password = Faker.Lorem.Sentence(1);

            return userForCreationDto;
        }

        private AttachmentForCreationDto CreateRandomAttachment(AttachmentForCreationDto attachmentForCreationDto)
        {
            attachmentForCreationDto.FileName = Guid.NewGuid().ToString("N") + ".png";
            attachmentForCreationDto.Stream = File.OpenRead("../../../TestImage/image.png");
            EnvironmentHelper.WebRootPath = "../../../wwwrootTest";
            return attachmentForCreationDto;
        }
    }
}
