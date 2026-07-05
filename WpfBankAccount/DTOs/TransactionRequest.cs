using BankAccountCore;

namespace WpfBankAccount.DTOs
{
    public class TransactionRequest
    {
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
