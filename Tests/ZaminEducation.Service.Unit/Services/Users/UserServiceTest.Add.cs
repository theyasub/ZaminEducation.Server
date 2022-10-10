using FluentAssertions;
using Force.DeepCloner;
using System.Threading.Tasks;
using ZaminEducation.Domain.Entities.Users;
using ZaminEducation.Service.DTOs.Commons;
using ZaminEducation.Service.DTOs.Users;

namespace ZaminEducation.Test.Unit.Services.Users
{
    public partial class UserServiceTest
    {


        [Fact]
        public async Task ShouldCreateUser()
        {
            // given
            UserForCreationDto randomUser = CreateRandomUser(new UserForCreationDto());
            UserForCreationDto inputUser = randomUser;
            UserForCreationDto expectedUser = inputUser.DeepClone();

            // when
            User actualUser = await userService.CreateAsync(inputUser);

            // then
            actualUser.Should().NotBeNull();

            actualUser.Username.Should().BeEquivalentTo(expectedUser.Username);
        }

        [Fact]
        public async Task ShouldCreateUserWithAttachment()
        {
            // given
            UserForCreationDto randomUser = CreateRandomUser(new UserForCreationDto());
            AttachmentForCreationDto randomAttachment = CreateRandomAttachment(new AttachmentForCreationDto());
            UserForCreationDto inputUser = randomUser;
            AttachmentForCreationDto inputAttachment = randomAttachment;
            UserForCreationDto expectedUser = inputUser.DeepClone();
            AttachmentForCreationDto expetedAttachment = inputAttachment.DeepClone();

            // when
            User actualUser = await userService.CreateAsync(inputUser);
            await userService.AddAttachmentAsync(actualUser.Id, inputAttachment);

            User actualUserWithAttachment = await userService.GetAsync(u => u.Id == actualUser.Id);

            // then
            actualUser.Should().NotBeNull();
            actualUserWithAttachment.Should().NotBeNull();
            actualUserWithAttachment.Image.Should().NotBeNull();

            actualUser.Username.Should().BeEquivalentTo(expectedUser.Username);
        }
    }
}
