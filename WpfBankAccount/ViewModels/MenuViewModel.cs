using BankAccountCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WpfBankAccount.Navigation;

namespace WpfBankAccount.ViewModels
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;

        public BankAccount LoggedInAccount { get; }
        public ICommand LogoutCommand { get; }
        public ICommand CheckBalanceCommand { get; }
        public ICommand DepositCommand { get; }

        public MenuViewModel(INavigationService navigationService, BankAccount loggedInAccount)
        {
            _navigationService = navigationService;
            LoggedInAccount = loggedInAccount;
            LogoutCommand = new RelayCommand(Logout, CanLogout);
            CheckBalanceCommand = new RelayCommand(CheckBalance, CanCheckBalance);
            DepositCommand = new RelayCommand(Deposit, CanDeposit);
        }

        private void Logout(object? parameter)
        {
            _navigationService.NavigateTo(ViewType.Login);
        }
        private bool CanLogout(object? parameter)
        {
            return true;
        }
        private void CheckBalance(object parameter)
        {
            _navigationService.NavigateTo(ViewType.CheckBalance, LoggedInAccount);
        }

        private bool CanCheckBalance(object parameter)
        {
            return true;
        }
        private void Deposit(object parameter)
        {
            _navigationService.NavigateTo(ViewType.Deposit, LoggedInAccount);
        }
        private bool CanDeposit(object parameter)
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
