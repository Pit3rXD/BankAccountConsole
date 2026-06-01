using BankAccountCore;
using BankApp.Api.DTOs;
using BankApp.Api.Interfaces;
using BankApp.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankApp.Api.Services
{
    public class AuthService : Interfaces.IAuthService
    {

        private readonly IBankAccountRepository _repository;
        private readonly IAccountNumberGenerator _generator;
        private readonly IConfiguration _configuration;

        public AuthService(IBankAccountRepository repository, IAccountNumberGenerator generator,
            IConfiguration configuration)
        {
            _repository = repository;
            _generator = generator;
            _configuration = configuration;
        }
        public async Task<string> Login(LoginDto dto)
        {
            var username = dto.UserName;
            var user = await _repository.GetByUsernameAsync(username);
            if (user == null)
            {
                throw new InvalidCredentialsException();
            }
            var password = dto.Password;
            var hashedPassword = user.Password;
            bool isValid = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            if (!isValid)
            {
                throw new InvalidCredentialsException();
            }
            var secret = _configuration["Jwt:Secret"]!;
            var issuer = _configuration["Jwt:Issuer"]!;
            var audience = _configuration["Jwt:Audience"]!;
            var expireMinutes = _configuration["Jwt:ExpireMinutes"]!;

            var key = new  SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(expireMinutes)),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task Register(RegisterDto dto)
        {
            var username = dto.UserName;
            var user = await _repository.GetByUsernameAsync(username);
            if (user != null)
            {
                throw new UserAlreadyExistsException();
            }
            var password = dto.Password;
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var accountNumber = _generator.Generate();
            var newUser = new BankAccountEntity
            {
                UserName = dto.UserName,
                OwnerName = dto.OwnerName,
                Password = passwordHash,
                AccountNumber = accountNumber
            };
            await _repository.CreateAsync(newUser);
        }
    }
}

