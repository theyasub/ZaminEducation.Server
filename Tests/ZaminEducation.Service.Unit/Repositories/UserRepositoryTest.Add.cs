using AutoMapper;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using System;
using System.Threading.Tasks;
using ZaminEducation.Data.IRepositories;
using ZaminEducation.Domain.Entities.Commons;
using ZaminEducation.Domain.Entities.Users;
using ZaminEducation.Domain.Enums;
using ZaminEducation.Service.DTOs.Users;
using ZaminEducation.Service.Mappers;

namespace ZaminEducation.Service.Test.Unit.Repositories
{
    public partial class UserRepositoryTest
    {
        private readonly Mock<IRepository<User>> userRepositoryMock;
        private readonly Mock<IRepository<Attachment>> attachmentRepositoryMock;
        private readonly IMapper mapper;

        public UserRepositoryTest()
        {
            userRepositoryMock = new Mock<IRepository<User>>();
            attachmentRepositoryMock = new Mock<IRepository<Attachment>>();
            mapper = new MapperConfiguration(m =>
            {
                m.AddProfile<MappingProfile>();
            }).CreateMapper();
        }

        [Fact]

        public async Task ShouldCreateUser()
        {
            // given
            User randomUser = CreateRandomUser(new UserForCreationDto());
            User inputUser = randomUser;
            User insertedUser = inputUser;
            User expectedUser = insertedUser.DeepClone();

            userRepositoryMock.Setup(
                ur =>
                ur.AddAsync(inputUser))
                    .ReturnsAsync(insertedUser);
            // when

            User actualUser = await userRepositoryMock.Object.AddAsync(inputUser);

            // then
            actualUser.Should().BeEquivalentTo(expectedUser);
        }

        [Fact]
        public async Task ShouldCreateUserWithAddress()
        {
            // given
            User randomUser = CreateRandomUserWithAddress(new User());
            User inputUser = randomUser;
            User insertedUser = inputUser;
            User expectedUser = insertedUser.DeepClone();

            userRepositoryMock.Setup(
                ur =>
                ur.AddAsync(inputUser))
                    .ReturnsAsync(insertedUser);
            // when

            User actualUser = await userRepositoryMock.Object.AddAsync(inputUser);

            // then
            actualUser.Should().BeEquivalentTo(expectedUser);
        }


        private User CreateRandomUser(UserForCreationDto userForCreationDto)
        {
            userForCreationDto.FirstName = Faker.Name.First();
            userForCreationDto.LastName = Faker.Name.Last();
            userForCreationDto.Username = Faker.Name.Middle();
            userForCreationDto.AddressId = null;
            userForCreationDto.Gender = (Gender)Faker.RandomNumber.Next(0, 1);
            userForCreationDto.Bio = Faker.Lorem.Sentence(1);
            userForCreationDto.DateOfBirth = DateTime.UtcNow;
            userForCreationDto.Password = Faker.Lorem.Sentence(1);

            return mapper.Map<User>(userForCreationDto);
        }


        private User CreateRandomUserWithAddress(User user)
        {
            user.FirstName = Faker.Name.First();
            user.LastName = Faker.Name.Last();
            user.Username = Faker.Name.Middle();
            user.Gender = (Gender)Faker.RandomNumber.Next(0, 1);
            user.Bio = Faker.Lorem.Sentence(1);
            user.DateOfBirth = DateTime.UtcNow;
            user.Password = Faker.Lorem.Sentence(1);
            user.Address = new Address
            {
                AddressLine = Faker.Address.StreetAddress(),
                Country = new Region()
                {
                    Name = Faker.Country.Name()
                },
                Region = new Region()
                {
                    Name = Faker.Address.City()
                },
            };

            return user;
        }
    }
}
