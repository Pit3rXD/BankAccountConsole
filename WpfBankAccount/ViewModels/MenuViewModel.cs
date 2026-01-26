using BankAccountCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfBankAccount.ViewModels
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        private readonly BankAccount _loggedInAccount;
        private readonly MainViewModel _mainViewModel;
        private readonly Action<object> _navigate;

        public BankAccount LoggedInAccount { get; }
        public MenuViewModel(Action<object> navigate, BankAccount loggedInAccount)
        {
            _navigate = navigate;
            _loggedInAccount = loggedInAccount;
            LoggedInAccount = loggedInAccount;
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
