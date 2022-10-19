using FluentAssertions;
using System.Threading.Tasks;

namespace ZaminEducation.Test.Unit.Services.YouTubeCourse
{
    public partial class YoutubeServiceAndCourseServiceTest
    {
        [Fact]
        public async ValueTask ShouldModifyCommentById()
        {
            // given
            var dependencies = await CreateAllDependencies();

            // when
            var actualComment =
                await courseCommentService.CreateAsync(dependencies.CourseId, Faker.Lorem.Sentence(3));

            var actualModifiedComment =
                await courseCommentService.UpdateAsync(actualComment.Id, Faker.Lorem.Sentence(4));
            // then
            actualComment.Should().NotBeNull();
            actualModifiedComment.Should().NotBeNull();
            actualModifiedComment.Should().NotBeEquivalentTo(actualComment);
        }
    }
}
