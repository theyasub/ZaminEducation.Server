using FluentAssertions;
using Force.DeepCloner;
using System.Linq;
using System.Threading.Tasks;
using ZaminEducation.Service.DTOs.Courses;
using ZaminEducation.Service.DTOs.Users;

namespace ZaminEducation.Test.Unit.Services.YouTube
{
    public partial class YoutubeServiceAndCourseServiceTest
    {
        [Fact]
        public async ValueTask ShouldCreateYoutubePlaylist()
        {
            // given
            var dependencies = await CreateAllDependencies();

            // when
            var actualYoutubePlayList =
                await youTubeService.CreateRangeAsync(dependencies.YoutubePlaylistLink,
                    dependencies.CourseId,
                    dependencies.CourseModuleId);

            // then
            actualYoutubePlayList.Should().NotBeNull();
        }

        [Fact]
        public async ValueTask ShouldCreateRandomYoutubeVideo()
        {
            // given
            var dependencies = await CreateAllDependencies();

            // when
            var actualYoutubeVideo =
                await youTubeService.CreateAsync("https://www.youtube.com/watch?v=JxjXechMs4c", dependencies.CourseId);

            // then
            actualYoutubeVideo.Should().NotBeNull();
        }

        private async ValueTask<(string YoutubePlaylistLink, long CourseId, long CourseModuleId)> CreateAllDependencies()
        {
            var randomAuthor = CreateRandomAuthor(new UserForCreationDto());
            var randomCategory = CreateRandomCategory(new CourseCategoryForCreationDto());
            var randomCourse = CreateRandomCourse(new CourseForCreationDto());

            var inputAuthor = randomAuthor;
            var inputCategory = randomCategory;
            var inputCourse = randomCourse;

            var expectedAuthor = inputAuthor.DeepClone();
            var expectedCategory = inputCategory.DeepClone();
            var expectedCourse = inputCourse.DeepClone();

            // when
            var actualAuthor = await userService.CreateAsync(inputAuthor);
            actualAuthor.Should().NotBeNull();
            actualAuthor.Username.Should().BeEquivalentTo(expectedAuthor.Username);

            var actualCategory = await courseCategoryService.CreateAsync(inputCategory);
            actualCategory.Should().NotBeNull();
            actualCategory.Name.Should().BeEquivalentTo(expectedCategory.Name);

            inputCourse.AuthorId = actualAuthor.Id;
            inputCourse.CategoryId = actualCategory.Id;

            var actualCourse = await courseService.CreateAsync(inputCourse);

            // then
            actualCourse.Should().NotBeNull();
            actualCourse.Name.Should().BeEquivalentTo(expectedCourse.Name);

            var actualCourseModuleId = (await courseService.GetAsync(c => c.Id == actualCourse.Id)).Modules.FirstOrDefault().Id;

            return (actualCourse.YouTubePlaylistLink, actualCourse.Id, actualCourseModuleId);
        }
    }
}
