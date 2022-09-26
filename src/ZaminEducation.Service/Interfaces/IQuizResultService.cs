using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZaminEducation.Domain.Configurations;
using ZaminEducation.Domain.Entities.Quizzes;

namespace ZaminEducation.Service.Interfaces
{
    public interface IQuizResultService
    {
        ValueTask<QuizResult> GetAsync(Expression<Func<QuizResult, bool>> expression);
        ValueTask<IEnumerable<QuizResult>> GetAllAsync(Expression<Func<QuizResult, bool>> expression, PaginationParams @params);
    }
}
