using BankAccountCore;

namespace BankApp.Api.DTOs
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string CounterpartyAccountNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public decimal BalanceAfter { get; set; }
    }
}
