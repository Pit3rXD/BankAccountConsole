using System;

namespace BankAccountConsole
{
    internal enum TransactionType 
    { 
        Deposit, 
        Withdrawal 
    }
    internal class Transaction
    {
        public decimal Amount {  get; set; }
        public DateTime Date {  get; set; } 
        public TransactionType Type { get; set; }
    }
}
