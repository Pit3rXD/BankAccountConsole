using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BankAccountLibrary.Models
{
    public class BankAccount
    {
        private static int _accountNumberSeed = 10000000;

        private string _password;

        private List<Transaction> _transactions = new List<Transaction>();

        [JsonInclude]
        public string AccountNumber { get; private set; }

        public decimal Balance { get; set; }

        public string OwnerName { get; private set; }

        public string Password => _password;

        public List<Transaction> Transactions
        {
            get { return _transactions; }
            set
            {
                if (value != null)
                {
                    _transactions = value;
                }
                else
                {
                    _transactions = new List<Transaction>();
                }
            }
        }

        public string Username { get; private set; }

        public BankAccount(string ownerName, string username, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password cannot be empty.", nameof(password));
            }
            if (string.IsNullOrWhiteSpace(ownerName))
            {
                throw new ArgumentException("Name and surname cannot be empty.", nameof(ownerName));
            }
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username cannot be empty.", nameof(username));
            }
            OwnerName = ownerName;
            Username = username;
            _password = password;

            Balance = 0;
        }

        [JsonConstructor]
        public BankAccount(string accountNumber, string ownerName, string username, string password, decimal balance)
        {
            AccountNumber = accountNumber;
            OwnerName = ownerName;
            Username = username;
            _password = password;
            Balance = balance;
        }

        public void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public bool Authenticate(string password)
        {
            return _password == password;
        }

        public IEnumerable<Transaction> GetTransactionHistory()
        {
            List<Transaction> sortedTransacions = new List<Transaction>(_transactions);
            sortedTransacions.Sort((a, b) => b.Date.CompareTo(a.Date));
            return sortedTransacions;
        }

        public void SetAccountNumber(string accountNumber)
        {
            AccountNumber = accountNumber;
        }

        public override string ToString()
        {
            return $"{Username} ({OwnerName})";
        }
    }
}