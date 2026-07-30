
namespace WpfBankAccount.DTOs
{
    public class TransferRequest
    {
        public decimal Amount { get; set; }
        public string RecipientAccountNumber { get; set; } = string.Empty;
    }
}
