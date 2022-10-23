using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Users.Microservice.Data.IRepositories;
using Users.Microservice.Models.Entities;
using Users.Microservice.Models.Enums;
using Users.Microservice.Services.Extentions;
using Users.Microservice.Services.Interfaces;

namespace Users.Microservice.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> repository;
        private readonly IConfiguration configuration;

        public AuthService(IRepository<User> repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
        }

        public async Task<string> GenereteToken(string username, string password)
        {
            User user = await repository.GetAsync(u =>
                u.Username == username && u.Password.Equals(password.Encrypt()) && u.State != ItemState.Deleted);

            if (user is null)
                throw new Exception("Login or Password is incorrect");

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            byte[] tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

            SecurityTokenDescriptor tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(configuration["JWT:lifetime"])),
                Issuer = configuration["JWT:Issuer"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
    }
}
