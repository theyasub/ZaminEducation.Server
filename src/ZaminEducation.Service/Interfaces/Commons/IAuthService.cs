namespace ZaminEducation.Service.Interfaces.Commons
{
    public interface IAuthService
    {
        Task<string> GenerateToken(string username, string password);
    }
}
