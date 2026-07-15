using BankAccountCore;
using System.Net.Http;
using System.Windows.Input;
using WpfBankAccount.DTOs;
using WpfBankAccount.Interfaces;

namespace WpfBankAccount.ViewModels
{
    public class DepositViewModel : TransactionViewModelBase
    {
        private readonly IApiService _apiService;
        public ICommand DepositCommand { get; }
        public DepositViewModel(INavigationService navigationService, WpfBankAccount.DTOs.BankAccountDto account, IApiService apiService)
            : base(account, navigationService)
        {
            _apiService = apiService;
            DepositCommand = new RelayCommand(ExecuteDeposit, CanExecuteDeposit);
        }
        private async void ExecuteDeposit(object parameter)
        {
            try
            {
                var request = new TransactionRequest { Amount = Amount, TransactionType = TransactionType.Deposit };
                var response = await _apiService.CreateTransactionAsync(request, Account.Id);
                Account.Balance = response.BalanceAfter;
                OnPropertyChanged(nameof(Balance));
                ErrorMessage = string.Empty;
                Amount = 0;

            }
            catch (HttpRequestException ex)
            {
                ErrorMessage = $"Operation was canceled: {ex.Message}";
            }
        }
        private bool CanExecuteDeposit(object parameter)
        {
            return Amount > 0;
        }
    }
}
