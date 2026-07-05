using BankApp.Api.DTOs;

namespace BankApp.Api.Interfaces
{
    public interface IAuthService
    {
        Task Register(RegisterDto dto);
        Task<LoginResponseDto> Login(LoginDto dto);
    }
}
