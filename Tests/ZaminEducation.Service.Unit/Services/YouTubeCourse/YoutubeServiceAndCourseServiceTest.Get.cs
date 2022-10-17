using FluentAssertions;
using System.Threading.Tasks;
using ZaminEducation.Domain.Configurations;

namespace ZaminEducation.Test.Unit.Services.YouTubeCourse
{
    public partial class YoutubeServiceAndCourseServiceTest
    {
        [Fact]
        public async ValueTask ShouldGetYoutubePlaylist()
        {
            // given
            var dependencies = await CreateAllDependencies();

            // when
            var actualYoutubePlayList =
                await youTubeService.CreateRangeAsync(dependencies.YoutubePlaylistLink,
                    dependencies.CourseId,
                    dependencies.CourseModuleId);

            var actullyGotPlayList = youTubeService.GetAllAsync(
                    new PaginationParams()
                    {
                        PageIndex = 0,
                        PageSize = 0
                    });

            var actuallyGotCourses = await courseService.GetAllAsync(
                    new PaginationParams() { PageIndex = 0, PageSize = 0 });
            // then
            actuallyGotCourses.Should().NotBeNull();
            actualYoutubePlayList.Should().NotBeNull();
            actullyGotPlayList.Should().NotBeNull();
            actullyGotPlayList.Should().BeEquivalentTo(actualYoutubePlayList);
        }

        [Fact]
        public async ValueTask ShouldGetYoutubeVideo()
        {
            var dependencies = await CreateAllDependencies();

            // when
            var actualYoutubeVideo =
                await youTubeService.CreateAsync(dependencies.YoutubePlaylistLink,
                    dependencies.CourseId);

            var actullyGotVideo = youTubeService.GetAsync(y => y.Id == actualYoutubeVideo.Id);


            var actuallyGotCourses = await courseService.GetAllAsync(
                    new PaginationParams() { PageIndex = 0, PageSize = 0 });
            // then
            actuallyGotCourses.Should().NotBeNull();
            actualYoutubeVideo.Should().NotBeNull();
            actullyGotVideo.Should().NotBeNull();
            actullyGotVideo.Should().BeEquivalentTo(actualYoutubeVideo);
        }
    }
}
