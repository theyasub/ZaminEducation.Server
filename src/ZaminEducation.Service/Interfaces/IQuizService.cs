using System.Linq.Expressions;
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
        ValueTask<Quiz> UpdateAsync(long quizId,
            QuizForCreationDto quizForCreationDto,
            QuizContentForCreationDto questionDto);
        ValueTask<QuestionAnswer> UpdateAnswerAsync(long answerId, QuestionAnswerForCreationDto answerForCreationDto);
        ValueTask<bool> DeleteAnswerAsync(Expression<Func<QuestionAnswer, bool>> expression);
        ValueTask<bool> DeleteAsync(Expression<Func<Quiz, bool>> expression);
        ValueTask<bool> DeleteAssetAsync(Expression<Func<QuizAsset, bool>> expression);
    }
}