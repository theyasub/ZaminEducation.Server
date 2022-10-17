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
        public async ValueTask ShouldUpdateYoutubePlaylist()
        {
            var randomAuthor = CreateRandomAuthor(new UserForCreationDto());
            var randomCategory = CreateRandomCategory(new CourseCategoryForCreationDto());
            var randomCourse = CreateRandomCourse(new CourseForCreationDto());

            var expectedCourse = randomCourse.DeepClone();

            // when
            var actualAuthor = await userService.CreateAsync(randomAuthor);
            var actualCategory = await courseCategoryService.CreateAsync(randomCategory);
            randomCourse.AuthorId = actualAuthor.Id;
            randomCourse.CategoryId = actualCategory.Id;

            var actualCourse = await courseService.CreateAsync(randomCourse);

            randomCourse.Name = Faker.Name.FullName();

            var actuallyUpdatedCourse = await courseService.UpdateAsync(
                    c => c.Id == actualCourse.Id, randomCourse);

            var actualCourseModuleId = (await courseService.GetAsync(c => c.Id == actualCourse.Id)).Modules.FirstOrDefault().Id;

            var actualYoutubePlayList =
                await youTubeService.CreateRangeAsync(actualCourse.YouTubePlaylistLink,
                    actualCourse.Id,
                    actualCourseModuleId);
            // 

            // then
            actualCourse.Should().NotBeNull();
            actualCourse.Name.Should().BeEquivalentTo(expectedCourse.Name);

            actualCourse.Name.Should().NotBeEquivalentTo(actuallyUpdatedCourse.Name);
        }
    }
}
