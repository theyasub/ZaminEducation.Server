
namespace ZaminEducation.Service.ViewModels.Quizzes;

public class UserQuizzesResultViewModel
{
    public double Persentage { get; set; }
    public int QuizzesCount { get; set; }
    public int AcceptedQuizzes { get; set; }

    public IEnumerable<QuizResultViewModel> quizResultViewModels { get; set; }
}