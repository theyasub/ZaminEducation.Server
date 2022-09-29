using AutoMapper;
using ZaminEducation.Data.IRepositories;
using ZaminEducation.Domain.Entities.Quizzes;
using ZaminEducation.Service.DTOs.Quizzes;
using ZaminEducation.Service.Interfaces;

namespace ZaminEducation.Service.Services
{
    public class QuizService : IQuizService
    {
        private readonly IRepository<Quiz> quizRepository;
        private readonly IRepository<QuizContent> quizContentRepository;
        private readonly IRepository<QuizAsset> quizAssetRepository;
        private readonly IRepository<QuestionAnswer> answerRepository;
        private readonly IMapper mapper;

        public QuizService(IRepository<Quiz> quizRepository,
            IRepository<QuizContent> quizContentRepository,
            IRepository<QuizAsset> quizAssetRepository,
            IRepository<QuestionAnswer> answerRepository,
            IMapper mapper)
        {
            this.quizRepository = quizRepository;
            this.quizContentRepository = quizContentRepository;
            this.quizAssetRepository = quizAssetRepository;
            this.answerRepository = answerRepository;
            this.mapper = mapper;
        }

        public async ValueTask<Quiz> CreateAsync(long courseId, QuizForCreationDto quizForCreationDto, QuizContentForCreationDto questionDto)
        {
            await quizContentRepository.AddAsync(
                mapper.Map<QuizContent>(questionDto));

            var quiz = await quizRepository.AddAsync(
                mapper.Map<Quiz>(quizForCreationDto));

            await quizRepository.SaveChangesAsync();

            return quiz;
        }

        public async ValueTask<QuestionAnswer> CreateAnswerAsync(QuestionAnswerForCreationDto answerForCreationDto)
        {
            var answer = await answerRepository.AddAsync(mapper.Map<QuestionAnswer>(answerForCreationDto));

            return answer;
        }

        public async ValueTask<QuizAsset> CreateAssetsAsync(QuizAssetForCreationDto assetForCreationDto)
        {
            var asset = await quizAssetRepository.AddAsync(mapper.Map<QuizAsset>(assetForCreationDto));

            return asset;
        }
    }
}
