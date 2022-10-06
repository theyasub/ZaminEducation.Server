using FluentAssertions;
using Force.DeepCloner;
using System.Threading.Tasks;
using ZaminEducation.Domain.Entities.Users;
using ZaminEducation.Service.DTOs.Users;

namespace ZaminEducation.Test.Unit.Services
{
    public partial class UserServiceTest
    {
        [Fact]
        public async Task ShoulDeleteUserById()
        {
            UserForCreationDto randomUser = CreateRandomUser(new UserForCreationDto());
            UserForCreationDto inputUser = randomUser;
            UserForCreationDto expectedUser = inputUser.DeepClone();
            UserForCreationDto inputUserForUpdate = inputUser.DeepClone();
            inputUserForUpdate.LastName = Faker.Name.Last();
            // when
            User actualUser = await userService.CreateAsync(inputUser);

            bool isDeleted = await userService.DeleteAsync(u => u.Id == actualUser.Id);

            // then
            actualUser.Should().NotBeNull();

            actualUser.Username.Should().BeEquivalentTo(expectedUser.Username);
            isDeleted.Should().Be(true);
        }

        [Fact]
        public async Task ShoulDeleteUserByUserName()
        {
            UserForCreationDto randomUser = CreateRandomUser(new UserForCreationDto());
            UserForCreationDto inputUser = randomUser;
            UserForCreationDto expectedUser = inputUser.DeepClone();
            UserForCreationDto inputUserForUpdate = inputUser.DeepClone();
            inputUserForUpdate.LastName = Faker.Name.Last();
            // when
            User actualUser = await userService.CreateAsync(inputUser);

            bool isDeleted = await userService.DeleteAsync(u => u.Username == actualUser.Username);

            // then
            actualUser.Should().NotBeNull();

            actualUser.Username.Should().BeEquivalentTo(expectedUser.Username);
            isDeleted.Should().Be(true);
        }
    }
}
