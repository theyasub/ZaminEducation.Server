using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZaminEducation.Data.IRepositories;
using ZaminEducation.Domain.Configurations;
using ZaminEducation.Domain.Entities.Quizzes;
using ZaminEducation.Service.Exceptions;
using ZaminEducation.Service.Extensions;
using ZaminEducation.Service.Interfaces;

namespace ZaminEducation.Service.Services
{
    public class QuizResultService : IQuizResultService
    {
        private readonly IRepository<QuizResult> repository;

        public QuizResultService(IRepository<QuizResult> repository)
        {
            this.repository = repository;
        }

        public async ValueTask<IEnumerable<QuizResult>> GetAllAsync
            (Expression<Func<QuizResult, bool>> expression, PaginationParams @params)
        {
            var pagedList = repository.GetAll(expression, new string[] { "User", "Course" }, false).ToPageList(@params);

            return pagedList;

            
        }
        public async ValueTask<QuizResult> GetAsync(Expression<Func<QuizResult, bool>> expression)
        {
            var existQuizResult = await repository.GetAsync(expression, new string[] {"User", "Course"});

            if (existQuizResult is null)
                throw new ZaminEducationException(404, "QuizResult not found.");

            return existQuizResult; 
        }
    }
}
