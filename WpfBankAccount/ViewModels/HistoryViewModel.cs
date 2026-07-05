using WpfBankAccount.DTOs;
using System.Collections.ObjectModel;

namespace WpfBankAccount.ViewModels
{
    public class HistoryViewModel : TransactionViewModelBase
    {
        //TODO: docelowo pobierać przez IApiService.GetAllByAccountIdAsync
        public HistoryViewModel(INavigationService navigationService, BankAccountDto account)
            : base(account, navigationService)
        {
            Transactions = new ObservableCollection<TransactionResponse>();
        }
        public ObservableCollection<TransactionResponse> Transactions { get; }
    }
}
