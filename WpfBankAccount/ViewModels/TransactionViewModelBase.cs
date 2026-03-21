using BankAccountCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WpfBankAccount.Navigation;

namespace WpfBankAccount.ViewModels
{
    public abstract class TransactionViewModelBase : INotifyPropertyChanged
    {
        private readonly BankAccount _account;
        private readonly INavigationService _navigationService;
        private decimal _amount;
        private string _errorMessage;
        protected BankAccount Account => _account;

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
                ErrorMessage = string.Empty;
            }
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasError));
            }
        }
        protected TransactionViewModelBase(BankAccount account, INavigationService navigationService)
        {
            _account = account;
            _navigationService = navigationService;

            BackCommand = new RelayCommand(Back, CanBack);
        }
        protected virtual void Back(object parameter)
        {
            _navigationService.NavigateTo(ViewType.Menu, _account);
        }
        protected virtual bool CanBack(object parameter)
        {
            return true;
        }
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
        public ICommand BackCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
