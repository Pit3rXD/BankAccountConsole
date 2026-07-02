namespace BankAccountCore
{
    public interface IAccountRepository
    {
        void Save(List<BankAccountDto> accounts);
        List<BankAccountDto> Load();
    }
}
