using ZaminEducation.Domain.Entities.Quizzes;
using ZaminEducation.Service.DTOs.Quizzes;

namespace ZaminEducation.Service.Interfaces
{
    public interface IQuizService
    {
        ValueTask<Quiz> CreateAsync(long courseId,
            QuizForCreationDto quizForCreationDto,
            QuizContentForCreationDto questionDto);
        ValueTask<QuestionAnswer> CreateAnswerAsync(QuestionAnswerForCreationDto answerForCreationDto);
        ValueTask<QuizAsset> CreateAssetsAsync(QuizAssetForCreationDto assetForCreationDto);
    }
}