using FluentAssertions;
using Force.DeepCloner;
using System.IO;
using System.Threading.Tasks;
using ZaminEducation.Service.DTOs.Commons;
using ZaminEducation.Service.DTOs.Users;

namespace ZaminEducation.Test.Unit.Services.Users
{
    public partial class UserServiceTest
    {
        [Fact]
        public async ValueTask ShouldCreateUser()
        {
            // given
            UserForCreationDto randomUser = CreateRandomUser(new UserForCreationDto());
            UserForCreationDto inputUser = randomUser;
            UserForCreationDto expectedUser = inputUser.DeepClone();

            // when
            actualUser = await userService.CreateAsync(inputUser);

            // then
            actualUser.Should().NotBeNull();

            actualUser.Username.Should().BeEquivalentTo(expectedUser.Username);
        }

        [Fact]
        public async ValueTask ShouldCreateUserWithAttachment()
        {
            // given
            UserForCreationDto randomUser = CreateRandomUser(new UserForCreationDto());
            AttachmentForCreationDto randomAttachment = CreateRandomAttachment(new AttachmentForCreationDto());
            UserForCreationDto inputUser = randomUser;
            AttachmentForCreationDto inputAttachment = randomAttachment;
            UserForCreationDto expectedUser = inputUser.DeepClone();
            AttachmentForCreationDto expetedAttachment = inputAttachment.DeepClone();

            // when
            actualUser = await userService.CreateAsync(inputUser);
            await userService.AddAttachmentAsync(actualUser.Id, inputAttachment);

            var actualUserWithAttachment = await userService.GetAsync(u => u.Id == actualUser.Id);

            // then
            actualUser.Should().NotBeNull();
            actualUserWithAttachment.Should().NotBeNull();
            actualUserWithAttachment.Image.Should().NotBeNull();
            actualUser.Username.Should().BeEquivalentTo(expectedUser.Username);

            File.Delete(Path.Combine("../../../wwwrootTest/images", actualUserWithAttachment.Image.Name));
        }
    }
}
