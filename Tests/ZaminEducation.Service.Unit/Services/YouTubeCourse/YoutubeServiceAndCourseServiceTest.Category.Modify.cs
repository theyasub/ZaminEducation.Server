using FluentAssertions;
using System.Threading.Tasks;
using ZaminEducation.Service.DTOs.Courses;

namespace ZaminEducation.Test.Unit.Services.YouTubeCourse
{
    public partial class YoutubeServiceAndCourseServiceTest
    {
        [Fact]
        public async ValueTask ShouldUpdateCourseCategory()
        {
            var randomCategory = CreateRandomCategory(new CourseCategoryForCreationDto());

            // when
            var actuallyCreatedCategory = await courseCategoryService.CreateAsync(randomCategory);

            randomCategory.Name = Faker.Name.Last();

            var actuallyUpdatedCategory =
                await courseCategoryService.UpdateAsync(actuallyCreatedCategory.Id, randomCategory);

            // then
            actuallyUpdatedCategory.Should().NotBeNull();
            actuallyUpdatedCategory.Should().NotBeEquivalentTo(actuallyCreatedCategory);
        }
    }
}
