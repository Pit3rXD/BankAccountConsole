using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using BankAccountUWP.Helpers;

namespace BankAccountUWP.ViewModels
{
    public class MainMenuViewModel
    {
        public event Action LoginRequested;
        public event Action RegisterRequested;
        public event Action ExitRequested;

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand ExitCommand { get; }

        public MainMenuViewModel()
        {
            LoginCommand = new RelayCommand(OnLogin);
            RegisterCommand = new RelayCommand(OnRegister);
            ExitCommand = new RelayCommand(OnExit);
        }
        private void OnLogin() => LoginRequested?.Invoke();
        private void  OnRegister() => RegisterRequested?.Invoke();
        private void OnExit() => ExitRequested?.Invoke();
    }
}
