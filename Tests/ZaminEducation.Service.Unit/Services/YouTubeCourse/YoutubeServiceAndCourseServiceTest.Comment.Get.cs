using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using ZaminEducation.Domain.Configurations;

namespace ZaminEducation.Test.Unit.Services.YouTubeCourse
{
    public partial class YoutubeServiceAndCourseServiceTest
    {
        [Fact]
        public async ValueTask ShouldGetCommentById()
        {
            // given
            var dependencies = await CreateAllDependencies();

            // when
            var actualComment =
                await courseCommentService.CreateAsync(dependencies.CourseId, Faker.Lorem.Sentence(3));

            var actualGotComment =
                await courseCommentService.GetAsync(actualComment.Id);

            // then
            actualComment.Should().NotBeNull();
            actualGotComment.Should().NotBeNull();
            actualGotComment.Should().BeEquivalentTo(actualComment);
        }

        [Fact]
        public async ValueTask ShouldGetAllReplies()
        {
            // given
            var dependencies = await CreateAllDependencies();

            // when
            var actualComment =
                await courseCommentService.CreateAsync(dependencies.CourseId, Faker.Lorem.Sentence(3));

            int numberOfRepliedComments = Faker.RandomNumber.Next(1, 8);

            for (int i = 0; i < numberOfRepliedComments; i++)
            {
                await courseCommentService.CreateAsync(
                    dependencies.CourseId,
                    Faker.Lorem.Sentence(4),
                    actualComment.Id);
            }

            var repliedComments = await courseCommentService.GetReplies(actualComment.Id);

            // then
            actualComment.Should().NotBeNull();
            repliedComments.Should().NotBeNull();
            repliedComments.Count().Should().Be(numberOfRepliedComments);
        }

        [Fact]
        public async ValueTask ShoulGetAllComments()
        {
            var dependencies = await CreateAllDependencies();

            // when
            int numberOfRepliedComments = Faker.RandomNumber.Next(1, 8);

            for (int i = 0; i < numberOfRepliedComments; i++)
            {
                await courseCommentService.CreateAsync(
                    dependencies.CourseId,
                    Faker.Lorem.Sentence(4));
            }

            var actualComments =
                await courseCommentService.GetAllAsync(new PaginationParams() { PageIndex = 0, PageSize = 0 }, dependencies.CourseId);

            // then
            actualComments.Should().NotBeNull();
            actualComments.Count().Should().Be(numberOfRepliedComments);
        }
    }
}
