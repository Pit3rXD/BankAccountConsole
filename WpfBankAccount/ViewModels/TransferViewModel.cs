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
            }
        }
        private bool CanExecuteTransfer(object parameter)
        {
            return Amount > 0 && !string.IsNullOrWhiteSpace(_recipientAccountNumber) && _recipientAccountNumber != AccountNumber;

        }
    }
}
