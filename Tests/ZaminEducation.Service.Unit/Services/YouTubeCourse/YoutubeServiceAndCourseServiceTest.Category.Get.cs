using FluentAssertions;
using System.Threading.Tasks;
using ZaminEducation.Domain.Configurations;
using ZaminEducation.Service.DTOs.Courses;

namespace ZaminEducation.Test.Unit.Services.YouTubeCourse
{
    public partial class YoutubeServiceAndCourseServiceTest
    {
        [Fact]
        public async ValueTask ShouldGetCategoryById()
        {
            var randomCategory = CreateRandomCategory(new CourseCategoryForCreationDto());

            // when
            var actuallyCreatedCategory = await courseCategoryService.CreateAsync(randomCategory);

            randomCategory.Name = Faker.Name.Last();

            var actuallyGotCategory =
                await courseCategoryService.GetAsync(cc => cc.Id == actuallyCreatedCategory.Id);

            // then
            actuallyGotCategory.Should().NotBeNull();
            actuallyGotCategory.Should().BeEquivalentTo(actuallyCreatedCategory);
        }

        [Fact]
        public async ValueTask ShouldGetAllCategories()
        {
            var randomCategory = CreateRandomCategory(new CourseCategoryForCreationDto());

            // when
            await courseCategoryService.CreateAsync(randomCategory);

            randomCategory.Name = Faker.Name.Last();

            var actuallyGotCategories =
                await courseCategoryService.GetAllAsync(new PaginationParams() { PageIndex = 0, PageSize = 0 });

            // then
            actuallyGotCategories.Should().NotBeNull();
        }
    }
}
