using BankAccountCore;
using System.Configuration;
using System.Windows.Input;
using WpfBankAccount.Navigation;

namespace WpfBankAccount.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _ownerName;
        private string _userName;
        private string _password;
        private string _errorMessage;
        private readonly IAuthService _authService;

        public string OwnerName
        {
            get => _ownerName;
            set
            {
                _ownerName = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }
        public string Username
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
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
        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel(INavigationService navigationService, IAuthService authService)
            : base(navigationService, null)
        {
            _authService = authService;

            LoginCommand = new RelayCommand(Login, CanLogin);
            RegisterCommand = new RelayCommand(Register, CanRegister);
        }
        private void Login(object parameter)
        {
            try
            {
                string password = parameter as string ?? Password;
                var account = _authService.Login(Username, password);
                ErrorMessage = $"Welcome {account.OwnerName}, login successful!";
                _navigationService.NavigateTo(ViewType.Menu, account);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message; /// Zająć się łapaniem konkretnych wyjątków
                                           /// najlepiej  mapować na komunikaty UI
            }
        }
        private void Register(object parameter)
        {
            try
            {
                string password = parameter as string ?? Password;
                var account = _authService.Register("Username", Username, password);
                ErrorMessage = $"An account has been created for {account.OwnerName}";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        private bool CanLogin(object parameter)
        {
            string password = parameter as string ?? Password;
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(password);
        }
        private bool CanRegister(object parameter)
        {
            string password = parameter as string ?? Password;
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(password);
        }
    }
}
