namespace WpfBankAccount.DTOs
{
    public class TransferResponse
    {
        public decimal BalanceAfter { get; set; }
        public string CounterpartyAccountNumber { get; set; } = string.Empty;
    }
}
