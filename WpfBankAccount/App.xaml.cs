using BankAccountCore;
using System.Windows;
using WpfBankAccount.ViewModels;
using WpfBankAccount.Views;

namespace WpfBankAccount
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IAuthService authService = new AuthService();
            IViewModelFactory viewModelFactory = new ViewModelFactory(authService);
            INavigationService navigationService = new NavigationService(viewModelFactory);

            var mainViewModel = new MainViewModel(navigationService);
            var mainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };

            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
