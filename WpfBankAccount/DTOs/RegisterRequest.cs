namespace WpfBankAccount.DTOs
{
    public class RegisterRequest
    {
        public string OwnerName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
