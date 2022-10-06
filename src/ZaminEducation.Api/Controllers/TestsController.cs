using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZaminEducation.Domain.Configurations;
using ZaminEducation.Service.DTOs.Quizzes;
using ZaminEducation.Service.Interfaces;

namespace ZaminEducation.Api.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class TestsController : BaseController
    {
        private readonly IQuizService quizService;
        private readonly IQuizResultService quizResultService;

        public TestsController(IQuizService quizService, IQuizResultService quizResultService)
        {
            this.quizService = quizService;
            this.quizResultService = quizResultService;
        }

        /// <summary>
        /// Create test
        /// </summary>
        /// <param name="quizAndQuizContentDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<IActionResult> CreateAsync(QuizAndQuizContentForCreationDto quizAndQuizContentDto)
            => Ok(await this.quizService.CreateAsync(quizAndQuizContentDto.Quiz.CourseId,
                                            quizAndQuizContentDto.Quiz,
                                            quizAndQuizContentDto.QuizContent));

        /// <summary>
        /// Create answer
        /// </summary>
        /// <param name="questionAnswerForCreationDto"></param>
        /// <returns></returns>
        [HttpPost("answers")]
        public async ValueTask<IActionResult> CreateAnswerAsync(QuestionAnswerForCreationDto questionAnswerForCreationDto)
            => Ok(await this.quizService.CreateAnswerAsync(questionAnswerForCreationDto));

        /// <summary>
        /// Create test asset
        /// </summary>
        /// <param name="quizAssetForCreationDto"></param>
        /// <returns></returns>
        [HttpPost("/api/tests/assets")]
        public async ValueTask<IActionResult> CreateAssetAsync(QuizAssetForCreationDto quizAssetForCreationDto)
            => Ok(await this.quizService.CreateAssetsAsync(quizAssetForCreationDto));

        /// <summary>
        /// Update test
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quizAndQuizContentForCreationDto"></param>
        /// <returns></returns>
        [HttpPut("/api/tests/{testId}")]
        public async ValueTask<IActionResult> UpdateAsync(long testId,
                                                QuizAndQuizContentForCreationDto quizAndQuizContentForCreationDto)
            => Ok(await this.quizService.UpdateAsync(testId,
                                            quizAndQuizContentForCreationDto.Quiz,
                                            quizAndQuizContentForCreationDto.QuizContent));

        /// <summary>
        /// Update answer
        /// </summary>
        /// <param name="answerId"></param>
        /// <param name="questionAnswerForCreationDto"></param>
        /// <returns></returns>
        [HttpPut("/api/tests/answers/{answerId}")]
        public async ValueTask<IActionResult> UpdateAnswerAsync(long answerId,
                                                QuestionAnswerForCreationDto questionAnswerForCreationDto)
            => Ok(await this.quizService.UpdateAnswerAsync(answerId, questionAnswerForCreationDto));

        /// <summary>
        /// Remove test
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/api/tests/{testId}")]
        public async ValueTask<IActionResult> RemoveAsync([FromRoute(Name = "testId")]long id)
            => Ok(await this.quizService.DeleteAsync(q => q.Id == id));

        /// <summary>
        /// Remove asnwer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/api/tests/answers/{answerId}")]
        public async ValueTask<IActionResult> RemoveAnswerAsync([FromRoute(Name = "answerId")]long id)
            => Ok(await this.quizService.DeleteAnswerAsync(qa => qa.Id == id));

        /// <summary>
        /// Remove test asset
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/api/tests/assets/{assetId}")]
        public async ValueTask<IActionResult> RemoveAssetAsync([FromRoute(Name = "assetId")]long id)
            => Ok(await this.quizService.DeleteAssetAsync(qa => qa.Id == id));

        /// <summary>
        /// Check quiz result
        /// </summary>
        /// <param name="userSelectionDtos"></param>
        /// <returns></returns>
        [HttpPost("results")]
        public async ValueTask<IActionResult> CreateAsync(IEnumerable<UserSelectionDto> userSelectionDtos)
            => Ok(await this.quizResultService.CreateAsync(userSelectionDtos));

        /// <summary>
        /// Get quiz result
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("results/{userId}"), Authorize(Policy = "AllPolicy")]
        public async ValueTask<IActionResult> GetAsync([FromRoute(Name = "userId")] long id)
            => Ok(await this.quizResultService.GetAsync(qr => qr.UserId == id));

        /// <summary>
        /// Get all quiz results
        /// </summary>
        /// <param name="id"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpGet("results/{userId}/collection"), Authorize(Policy = "AllPolicy")]
        public async ValueTask<IActionResult> GetAllAsync([FromRoute(Name = "userId")] long id, [FromQuery] PaginationParams @params)
            => Ok(await this.quizResultService.GetAllAsync(qr => qr.UserId == id, @params));
    }
}
