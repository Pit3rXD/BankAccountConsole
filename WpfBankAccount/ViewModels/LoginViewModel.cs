using BankAccountCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WpfBankAccount.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _userName;
        private string _password;
        private string _errorMessage;
        private AuthService _authService;
        private readonly Action<object> _navigate;

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

        public LoginViewModel(Action<object> navigate)
        {
            _authService = new AuthService();
            _navigate = navigate;

            LoginCommand = new RelayCommand(Login, CanLogin);
            RegisterCommand = new RelayCommand(Register, CanRegister);
        }

        private void Login(object parameter)
        {
            try
            {
                string password = parameter as string ?? Password;
                var account = _authService.Login(Username, password);
                ErrorMessage = $"Welcome {account.OwnerName}, login successgul!";
                _navigate(new MenuViewModel(_navigate, account)); // czy to usunąć?
                // LoiginViewModel zna przez to MenuWiewModel i tworzy koleny VM
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        private void Register(object parameter)
        {
            try
            {
                string password = parameter as string ?? Password;
                var accoutn = _authService.Register("Name and surname", Username, password);
                ErrorMessage = $"An account has been created for {accoutn.OwnerName}";
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
