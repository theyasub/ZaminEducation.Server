using FluentAssertions;
using Force.DeepCloner;
using System.Threading.Tasks;
using ZaminEducation.Service.DTOs.Courses;
using ZaminEducation.Service.DTOs.Users;

namespace ZaminEducation.Test.Unit.Services.YouTube
{
    public partial class YoutubeServiceTest
    {
        [Fact]
        public async Task ShouldDeleteCourseById()
        {
            // given
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

            var deletedCourse = await courseService.DeleteAsync(c => c.Id == actualCourse.Id);

            // then

            actualCourse.Should().NotBeNull();
            deletedCourse.Should().Be(true);
        }
    }
}
