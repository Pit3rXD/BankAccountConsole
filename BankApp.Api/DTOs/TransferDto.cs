using System.ComponentModel.DataAnnotations;

namespace BankApp.Api.DTOs
{
    public class TransferDto
    {
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be  greater than 0.")]
        public decimal Amount { get; set; }
        [Required]
        public string RecipientAccountNumber { get; set; }
    }
}
