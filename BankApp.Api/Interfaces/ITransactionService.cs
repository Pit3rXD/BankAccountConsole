using BankApp.Api.DTOs;

namespace BankApp.Api.Interfaces
{
    public interface ITransactionService
    {
        public Task<TransactionDto> DepositAsync(decimal amount, int bankAccountId);
        public Task<TransactionDto> WithdrawalAsync(decimal amount, int bankAccountId);
        public Task<TransactionDto> TransferAsync(decimal amount, int bankAccountId, string recipientAccountNumber); 
        public Task<IEnumerable<TransactionDto>> GetAllByAccountIdAsync(int accountId);
    }
}
