using BankAccountUWP.Helpers;
using System.Windows.Input;

namespace WpfApp1.ViewModels
{
    public class MainMenuViewModel
    {
        public ICommand ExitCommand { get; }

        public ICommand LoginCommand { get; }

        public ICommand RegisterCommand { get; }

        public event Action ExitRequested;

        public event Action LoginRequested;

        public event Action RegisterRequested;

        public MainMenuViewModel()
        {
            LoginCommand = new RelayCommand((_) => OnLogin());
            RegisterCommand = new RelayCommand((_) => OnRegister());
            ExitCommand = new RelayCommand((_) => OnExit());
        }

        private void OnExit() => ExitRequested?.Invoke();

        private void OnLogin() => LoginRequested?.Invoke();

        private void OnRegister() => RegisterRequested?.Invoke();
    }
}