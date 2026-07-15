using BankAccountCore;

namespace WpfBankAccount.DTOs
{
    public class TransactionResponse
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public decimal BalanceAfter { get; set; }
    }
}
