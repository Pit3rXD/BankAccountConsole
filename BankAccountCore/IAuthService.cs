namespace BankAccountCore
{
    public interface IAuthService
    {
        BankAccountDto Login(string username, string password);
        BankAccountDto Register(string ownerName, string username, string password);
        void SaveCurrentState();
    }
}
