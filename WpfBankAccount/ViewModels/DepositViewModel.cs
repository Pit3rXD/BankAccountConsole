using BankAccountCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WpfBankAccount.ViewModels
{
    public class DepositViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly BankAccount _account;
        private readonly TransactionService _transactionService;
        private decimal _amount;
        private string _errorMessage;
        private bool _hasError;
        public string OwnerName => _account.OwnerName;
        public string AccountNumber => _account.AccountNumber;
        public decimal Balance => _account.Balance;
        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }
        public string ErrorMessage 
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }
        public bool HasError
        {
            get => _hasError;
            set
            {
                string.IsNullOrEmpty(ErrorMessage);
                OnPropertyChanged();
            }
        }
        public ICommand DepositCommand { get; }
        public ICommand BackCommand { get; }

        public DepositViewModel(INavigationService navigationService, BankAccount account, TransactionService transactionService)
        {
            _navigationService = navigationService;
            _account = account;
            _transactionService = transactionService;

            BackCommand = new RelayCommand(Back, CanBack);
            DepositCommand = new RelayCommand(ExecuteDeposit, CanExecuteDeposit);
        }
        private void ExecuteDeposit(object parameter)
        {
            try
            {
                _transactionService.Deposit(_account, Amount);
                ErrorMessage = string.Empty;
                Amount = 0;
            }
            catch (ArgumentException ex)
            {
                ErrorMessage = ex.Message; //Napisać swój wyjątek.
            }
        }
        private bool CanExecuteDeposit(object parameter)
        {
            return Amount > 0;
        }
        private void Back(object parameter)
        {
            _navigationService.NavigateTo(Navigation.ViewType.Menu, _account);
        }
        private bool CanBack(object parameter)
        {
            return true;
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
