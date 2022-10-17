using FluentAssertions;
using Force.DeepCloner;
using System.Threading.Tasks;
using ZaminEducation.Service.DTOs.Users;

namespace ZaminEducation.Test.Unit.Services.Users
{
    public partial class UserServiceTest
    {
        [Fact]
        public async ValueTask ShoulDeleteUserById()
        {
            //given
            UserForCreationDto randomUser = CreateRandomUser(new UserForCreationDto());
            UserForCreationDto inputUser = randomUser;
            UserForCreationDto expectedUser = inputUser.DeepClone();
            UserForCreationDto inputUserForUpdate = inputUser.DeepClone();
            inputUserForUpdate.LastName = Faker.Name.Last();
            // when
            actualUser = await userService.CreateAsync(inputUser);

            bool isDeleted = await userService.DeleteAsync(u => u.Id == actualUser.Id);

            // then
            actualUser.Should().NotBeNull();

            actualUser.Username.Should().BeEquivalentTo(expectedUser.Username);
            isDeleted.Should().Be(true);
        }

        [Fact]
        public async ValueTask ShoulDeleteUserByUserName()
        {
            //given
            UserForCreationDto randomUser = CreateRandomUser(new UserForCreationDto());
            UserForCreationDto inputUser = randomUser;
            UserForCreationDto expectedUser = inputUser.DeepClone();
            UserForCreationDto inputUserForUpdate = inputUser.DeepClone();
            inputUserForUpdate.LastName = Faker.Name.Last();
            // when
            actualUser = await userService.CreateAsync(inputUser);

            bool isDeleted = await userService.DeleteAsync(u => u.Username == actualUser.Username);

            // then
            actualUser.Should().NotBeNull();

            actualUser.Username.Should().BeEquivalentTo(expectedUser.Username);
            isDeleted.Should().Be(true);
        }
    }
}
