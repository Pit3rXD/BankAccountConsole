using BankAccountCore;

namespace WpfBankAccount.DTOs
{
    public class TransactionRequest
    {
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
    }
}
