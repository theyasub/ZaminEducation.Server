using FluentAssertions;
using System.Threading.Tasks;

namespace ZaminEducation.Test.Unit.Services.YouTubeCourse
{
    public partial class YoutubeServiceAndCourseServiceTest
    {
        [Fact]
        public async ValueTask ShouldDeleteCommentById()
        {
            // given
            var dependencies = await CreateAllDependencies();

            // when
            var actualComment =
                await courseCommentService.CreateAsync(dependencies.CourseId, Faker.Lorem.Sentence(3));

            var isDeleted = await courseCommentService.DeleteAsync(actualComment.Id);
            // then

            actualComment.Should().NotBeNull();
            isDeleted.Should().BeTrue();
        }

        [Fact]
        public async ValueTask ShouldDeleteParentCommentById()
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

            var isDeleted = await courseCommentService.DeleteAsync(actualComment.Id);
            // then
            isDeleted.Should().BeTrue();
            actualRepliedComment.Should().NotBeNull();
        }

        [Fact]
        public async ValueTask ShouldDeleteRepliedCommentById()
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

            var isDeleted = await courseCommentService.DeleteAsync(actualRepliedComment.Id);
            // then
            isDeleted.Should().BeTrue();
            actualComment.Should().NotBeNull();
        }
    }
}
