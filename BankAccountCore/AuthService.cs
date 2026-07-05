using System;
using System.Collections.Generic;

namespace BankAccountCore
{
    public class AuthService : IAuthService
    {
        private List<BankAccountDto> _registeredAccounts;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountNumberGenerator _accountNumberGenerator;

        public AuthService(IAccountRepository accountRepository, IAccountNumberGenerator accountNumberGenerator)
        {
            _accountRepository = accountRepository;
            _accountNumberGenerator = accountNumberGenerator;
            _registeredAccounts = _accountRepository.Load();
        }
        public IEnumerable<BankAccountDto> GetAllAccounts()
        {
            return _registeredAccounts;
        }
        public void SaveCurrentState()
        {
            _accountRepository.Save(_registeredAccounts);
        }
       
        public BankAccountDto Register(string ownerName, string username, string password)
        {
            foreach (var account in _registeredAccounts)
            {
                if (account.Username == username)
                {
                    throw new UserAlreadyExistsException();
                }
            }
            string accountNumber = _accountNumberGenerator.Generate();
            var newAccount = new BankAccountDto(accountNumber, ownerName, username, password);
            _registeredAccounts.Add(newAccount);
            _accountRepository.Save(_registeredAccounts);
            return newAccount;
        }
        public BankAccountDto Login(string username, string password)
        {
            BankAccountDto foundAccount = null;

            foreach (var account in _registeredAccounts)
            {
                if (account.Username == username)
                {
                    foundAccount = account;
                }
            }
            if (foundAccount == null || !foundAccount.Authenticate(password))
            {
                throw new InvalidCredentialsException();
            }
            return foundAccount;
        }
    }
}
