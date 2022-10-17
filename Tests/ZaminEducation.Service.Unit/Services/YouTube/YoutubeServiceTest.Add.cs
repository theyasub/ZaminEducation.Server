using FluentAssertions;
using System.Threading.Tasks;

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
    }
}
