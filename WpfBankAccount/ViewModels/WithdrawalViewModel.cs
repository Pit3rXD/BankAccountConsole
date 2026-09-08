using BankAccountCore;
using System.Net.Http;
using System.Windows.Input;
using WpfBankAccount.DTOs;
using WpfBankAccount.Interfaces;

namespace WpfBankAccount.ViewModels
{
    public class WithdrawalViewModel : TransactionViewModelBase
    {
        private readonly IApiService _apiService;
        public ICommand WithdrawalCommand { get; }
        public WithdrawalViewModel(INavigationService navigationService, BankAccountDto account, IApiService apiService)
            : base(account, navigationService)
        {
            WithdrawalCommand = new RelayCommand(ExecuteWithdrawal, CanExecuteWithdrawal);
            _apiService = apiService;
        }
        private async void ExecuteWithdrawal(object parameter)
        {
            try
            {
                var request = new TransactionRequest { Amount = Amount, TransactionType = TransactionType.Withdrawal };
                var response = await _apiService.CreateTransactionAsync(request, Account.Id);
                Account.Balance = response.BalanceAfter;
                OnPropertyChanged(nameof(Balance));
                ErrorMessage = string.Empty;
                Amount = 0;
            }
            catch (HttpRequestException ex)
            {
                ErrorMessage = $"Withdrawal failed: {ex.Message}";
            }
        }
        private bool CanExecuteWithdrawal(object parameter)
        {
            return Amount > 0;
        }
    }
}
