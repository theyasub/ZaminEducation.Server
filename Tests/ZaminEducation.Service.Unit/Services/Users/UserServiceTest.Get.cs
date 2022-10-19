using FluentAssertions;
using Force.DeepCloner;
using System.Linq;
using System.Threading.Tasks;
using ZaminEducation.Domain.Configurations;
using ZaminEducation.Domain.Entities.Users;
using ZaminEducation.Service.DTOs.Users;

namespace ZaminEducation.Test.Unit.Services.Users
{
    public partial class UserServiceTest
    {
        [Fact]
        public async ValueTask ShouldGetUserById()
        {
            // given
            UserForCreationDto randomUser = CreateRandomUser(new UserForCreationDto());
            UserForCreationDto inputUser = randomUser;
            UserForCreationDto expectedUser = inputUser.DeepClone();

            // when
            actualUser = await userService.CreateAsync(inputUser);
            User gotUser = await userService.GetAsync(u => u.Id == actualUser.Id);

            // then
            actualUser.Should().NotBeNull();
            gotUser.Should().NotBeNull();
            actualUser.Should().BeEquivalentTo(gotUser);
        }

        [Fact]
        public async ValueTask ShouldGetAllUsers()
        {
            // given
            UserForCreationDto randomUser = CreateRandomUser(new UserForCreationDto());
            UserForCreationDto inputUser = randomUser;
            UserForCreationDto expectedUser = inputUser.DeepClone();

            // when
            int numberOfUsersWhichShouldBeCreated = Faker.RandomNumber.Next(1, 5);

            for (int i = 0; i > numberOfUsersWhichShouldBeCreated; i++)
            {
                inputUser.Username = Faker.Lorem.Sentence(i);
                await userService.CreateAsync(inputUser);
            }

            var gotUsers = await userService.GetAllAsync(new PaginationParams() { PageIndex = 1, PageSize = 1 });

            // then
            gotUsers.Should().NotBeNull();
            gotUsers.Count().Should().Be(numberOfUsersWhichShouldBeCreated);
        }
    }
}
