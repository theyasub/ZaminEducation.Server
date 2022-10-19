using FluentAssertions;
using System.Threading.Tasks;
using ZaminEducation.Service.DTOs.Courses;

namespace ZaminEducation.Test.Unit.Services.YouTubeCourse
{
    public partial class YoutubeServiceAndCourseServiceTest
    {
        [Fact]
        public async ValueTask ShouldDeleteCourseCategory()
        {
            //given
            var randomCategory = CreateRandomCategory(new CourseCategoryForCreationDto());

            // when
            var actuallyCreatedCategory = await courseCategoryService.CreateAsync(randomCategory);

            var isDeleted = await courseCategoryService.DeleteAsync(cc => cc.Id == actuallyCreatedCategory.Id);

            // then
            isDeleted.Should().BeTrue();
        }
    }
}
