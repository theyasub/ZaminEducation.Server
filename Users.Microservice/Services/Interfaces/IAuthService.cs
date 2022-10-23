namespace Users.Microservice.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<string> GenereteToken(string userName, string password);
    }
}