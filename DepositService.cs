﻿using BankAccountLibrary.Models;
using System;

namespace BankAccountConsole
{
    internal class DepositService
    {
        public void Deposit(BankAccount account, decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("The amount should be greater than zero.", nameof(amount));
            }
            account.Balance += amount;
            account.AddTransaction(new Transaction
            {
                Amount = amount,
                Date = DateTime.Now,
                Type = TransactionType.Deposit
            });
        }
        public void Withdrawal(BankAccount account, decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("The amount should be greater than zero.", nameof(amount));
            }
            if (account.Balance < amount)
            {
                throw new InsufficientFundsException();
            }
            account.Balance -= amount;
            account.AddTransaction(new Transaction
            {
                Amount = amount,
                Date = DateTime.Now,
                Type = TransactionType.Withdrawal
            });
        }
    }
}
