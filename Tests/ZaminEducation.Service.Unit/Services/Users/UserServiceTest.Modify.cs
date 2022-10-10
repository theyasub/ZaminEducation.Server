using FluentAssertions;
using Force.DeepCloner;
using System.Threading.Tasks;
using ZaminEducation.Domain.Entities.Users;
using ZaminEducation.Service.DTOs.Users;

namespace ZaminEducation.Test.Unit.Services.Users
{
    public partial class UserServiceTest
    {
        [Fact]
        public async Task ShouldUpdateUser()
        {
            // given
            UserForCreationDto randomUser = CreateRandomUser(new UserForCreationDto());
            UserForCreationDto inputUser = randomUser;
            UserForCreationDto expectedUser = inputUser.DeepClone();
            UserForUpdateDto inputUserForUpdate = mapper.Map<UserForUpdateDto>(expectedUser);

            inputUserForUpdate.LastName = Faker.Name.Last();

            // when
            User actualUser = await userService.CreateAsync(inputUser);

            User actualUpdatedUser = await userService.UpdateAsync(actualUser.Id, inputUserForUpdate);

            // then
            actualUser.Should().NotBeNull();
            actualUpdatedUser.Should().NotBeNull();

            actualUser.Username.Should().BeEquivalentTo(expectedUser.Username);
            actualUpdatedUser.Username.Should().BeEquivalentTo(inputUserForUpdate.Username);
        }
    }
}
