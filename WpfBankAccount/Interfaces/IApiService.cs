using WpfBankAccount.DTOs;

namespace WpfBankAccount.Interfaces
{
    public interface IApiService
    {
        Task Register(RegisterRequest request);
        Task <LoginResponse>Login(LoginRequest request);
        Task<BankAccountDto> CreateAccountAsync(BankAccountDto dto);
        Task<BankAccountDto?> GetByIdAsync(int id);
        Task<BankAccountDto> UpdateAsync(BankAccountDto dto);
        Task<TransactionResponse> CreateTransactionAsync(TransactionRequest request, int bankAccountId);
        Task<IEnumerable<TransactionResponse>> GetAllByAccountIdAsync(int bankAccountId);
    }
}
