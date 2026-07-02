using WpfBankAccount.DTOs;
using System.Collections.ObjectModel;

namespace WpfBankAccount.ViewModels
{
    public class HistoryViewModel : TransactionViewModelBase
    {
        public HistoryViewModel(INavigationService navigationService, BankAccountDto account)
            : base(account, navigationService)
        {
            Transactions = new ObservableCollection<Transaction>(account.GetTransactionHistory());
        }
        public ObservableCollection<Transaction> Transactions { get; }
    }
}
