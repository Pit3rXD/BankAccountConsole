namespace BankApp.Api.Exceptions
{
    public class TransferToOneselfException : Exception
    {
        public TransferToOneselfException() : base("You are transferring funds to yourself") { }
    }
}
