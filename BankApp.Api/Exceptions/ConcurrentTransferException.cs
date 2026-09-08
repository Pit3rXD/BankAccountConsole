namespace BankApp.Api.Exceptions
{
    public class ConcurrentTransferException : Exception
    {
        public ConcurrentTransferException() : base("The account was modified by another transaction. Please try again.") { }
    }
}
