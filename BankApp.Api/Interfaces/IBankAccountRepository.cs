using BankApp.Api.Models;

namespace BankApp.Api.Interfaces
{
    public interface IBankAccountRepository
    {
        Task<BankAccountEntity> CreateAsync(BankAccountEntity bankAccountEntity);
        Task<BankAccountEntity?> GetByIdAsync(int id);
        Task<BankAccountEntity?> GetByUsernameAsync(string username);
        Task<IEnumerable<BankAccountEntity>> GetAllAsync();  
        Task<BankAccountEntity> UpdateAsync(BankAccountEntity bankAccountEntity);
        Task<BankAccountEntity?> GetByAccountNumberAsync(string accountNumber);
        Task<bool> ExistsAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
