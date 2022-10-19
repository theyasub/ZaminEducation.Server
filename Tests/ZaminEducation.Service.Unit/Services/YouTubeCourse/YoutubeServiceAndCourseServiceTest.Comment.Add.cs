using FluentAssertions;
using System.Threading.Tasks;

namespace ZaminEducation.Test.Unit.Services.YouTubeCourse
{
    public partial class YoutubeServiceAndCourseServiceTest
    {
        [Fact]
        public async ValueTask ShouldCreateComment()
        {
            // given
            var dependencies = await CreateAllDependencies();

            // when
            var actualComment =
                await courseCommentService.CreateAsync(dependencies.CourseId, Faker.Lorem.Sentence(3));
            // then
            actualComment.Should().NotBeNull();
        }

        [Fact]
        public async ValueTask ShouldCreateRepliedComment()
        {
            // given
            var dependencies = await CreateAllDependencies();

            // when
            var actualComment =
                await courseCommentService.CreateAsync(dependencies.CourseId, Faker.Lorem.Sentence(3));

            var actualRepliedComment =
                await courseCommentService.CreateAsync(
                    dependencies.CourseId,
                    Faker.Lorem.Sentence(4),
                    actualComment.Id);

            // then
            actualComment.Should().NotBeNull();
            actualRepliedComment.Should().NotBeNull();
        }
    }
}
