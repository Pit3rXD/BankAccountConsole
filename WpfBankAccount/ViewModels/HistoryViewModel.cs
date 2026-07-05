using System.Collections.ObjectModel;
using System.Net.Http;
using WpfBankAccount.DTOs;
using WpfBankAccount.Interfaces;

namespace WpfBankAccount.ViewModels
{
    public class HistoryViewModel : TransactionViewModelBase
    {
        private readonly IApiService _apiService;

        public HistoryViewModel(INavigationService navigationService, BankAccountDto account, IApiService apiService)
            : base(account, navigationService)
        {
            Transactions = new ObservableCollection<TransactionResponse>();
            _apiService = apiService;
            _ = LoadTransactionAsync();
        }
        public ObservableCollection<TransactionResponse> Transactions { get; }

        private async Task LoadTransactionAsync()
        {
            try
            {
                var transactions = await _apiService.GetAllByAccountIdAsync(Account.Id);
                foreach (var transaction in transactions)
                {
                    Transactions.Add(transaction);
                }
            }
            catch (HttpRequestException ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
