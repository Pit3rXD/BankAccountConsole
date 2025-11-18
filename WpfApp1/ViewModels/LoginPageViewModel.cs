using BankAccountLibrary.Services;
using BankAccountUWP.Helpers;
using System.Windows.Input;

namespace WpfApp1.ViewModels
{
    public class LoginPageViewModel
    {
        private readonly AuthService authService;
        public ICommand LoginClicked { get; }

        public string Password { get; set; }

        public string Username
        {
            get;
            set;
        }

        public LoginPageViewModel(AuthService authService)
        {
            this.authService = authService;
            LoginClicked = new RelayCommand((_) => OnLoginClicked());
        }

        private void OnLoginClicked()
        {
            authService.Login(Username, Password);
        }
    }
}