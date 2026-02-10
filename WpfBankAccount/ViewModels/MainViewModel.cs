using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfBankAccount.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
