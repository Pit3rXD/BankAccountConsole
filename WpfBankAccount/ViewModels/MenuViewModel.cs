using BankAccountCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WpfBankAccount.Navigation;

namespace WpfBankAccount.ViewModels
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        private readonly MainViewModel _mainViewModel;
        private readonly INavigationService _navigationService;

        public BankAccount LoggedInAccount { get; }
        public ICommand LogoutCommand { get; }

        public MenuViewModel(INavigationService navigationService, BankAccount loggedInAccount)
        {
            _navigationService = navigationService;
            LoggedInAccount = loggedInAccount;
            LogoutCommand = new RelayCommand(Logout, CanLogout);
        }

        private void Logout(object? parameter)
        {
            _navigationService.NavigateTo(ViewType.Login);
        }
        private bool CanLogout(object? parameter)
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
