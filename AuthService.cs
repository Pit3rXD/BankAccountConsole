using System;
using System.Collections.Generic;

namespace BankAccountConsole
{
    internal class AuthService
    {
        private List<BankAccount> _registeredAccounts;

        public AuthService() 
        {
            _registeredAccounts = AccountDataService.LoadAccounts();
        }
        public IEnumerable<BankAccount> GetAllAccounts()
        {
            return _registeredAccounts;
        }

        public BankAccount Register(string ownerName, string username, string password)
        {
            foreach (var account in _registeredAccounts)
            {
                if (account.Username == username)
                {
                    throw new ArgumentException("A user whit this name already exists.");
                }
            }
            var newAccount = new BankAccount(ownerName, username, password);
            _registeredAccounts.Add(newAccount);
            AccountDataService.SaveAccounts(_registeredAccounts);
            return newAccount;
        }
        public BankAccount Login(string username, string password)
        {
            BankAccount foundAccount = null;

            foreach (var account in _registeredAccounts)
            {
                if (account.Username == username)
                {
                    foundAccount = account;
                }
            }
            if (foundAccount == null || !foundAccount.Authenticate(password))
            {
                throw new UnauthorizedAccessException("Incorrect login or password.");
            }
            return foundAccount;
        }
    }
}
