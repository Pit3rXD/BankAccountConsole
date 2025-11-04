using BankAccountLibrary.Services;
using BankAccountUWP.Helpers;
using System;
using System.Windows.Input;

namespace BankAccountUWP.ViewModels
{
    public class LoginPageViewModel
    {
        private readonly AuthService authService;
        public string Username 
        {
            get;
            set; 
        }
        public string Password { get; set; }

        public ICommand LoginClicked { get; }

        public LoginPageViewModel(AuthService authService)
        {
            this.authService = authService;
            LoginClicked = new RelayCommand(OnLoginClicked, (_) => Username != string.Empty && Password != string.Empty);
        }

        private void OnLoginClicked(object obj)
        {
            authService.Login(Username, Password);
        }
    }
}