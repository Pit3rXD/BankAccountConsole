using System.Net.Http;
using System.Windows.Input;
using WpfBankAccount.DTOs;
using WpfBankAccount.Interfaces;

namespace WpfBankAccount.ViewModels
{
    public class TransferViewModel : TransactionViewModelBase
    {
        private readonly IApiService _apiService;
        private string _recipientAccountNumber;
        public ICommand TransferCommand { get; }

        public TransferViewModel(INavigationService navigationService, BankAccountDto account, IApiService apiService)
            : base(account, navigationService)
        {
            TransferCommand = new RelayCommand(ExecuteTransfer, CanExecuteTransfer);
            _apiService = apiService;
        }

        public string RecipientAccountNumber
        {
            get => _recipientAccountNumber;
            set
            {
                _recipientAccountNumber = value;
                OnPropertyChanged();
                Validate();
            }
        }


        private async void ExecuteTransfer(object parameter)
        {
            try
            {
                var request = new TransferRequest { Amount = Amount, RecipientAccountNumber = _recipientAccountNumber };
                var response = await _apiService.CreateTransferAsync(request, Account.Id);
                Account.Balance = response.BalanceAfter;
                OnPropertyChanged(nameof(Balance));
                ErrorMessage = string.Empty;
                Amount = 0;
                RecipientAccountNumber = string.Empty;
            }
            catch (HttpRequestException ex)
            {
                ErrorMessage = $"Transfer failed: {ex.Message}";
            }
        }
        private bool CanExecuteTransfer(object parameter)
        {
            return Amount > 0 && !string.IsNullOrWhiteSpace(_recipientAccountNumber)
                && _recipientAccountNumber != AccountNumber && Amount <= Balance;

        }

        private void Validate()
        {
            if (AccountNumber == RecipientAccountNumber)
            {
                ErrorMessage = "You are transferring funds to yourself";
                return;
            }
            if (Amount > Balance)
            {
                ErrorMessage = "Insufficient funds";
                return;
            }
        }

        protected override void OnAmountChanged()
        {
            Validate();
        }
    }
}
