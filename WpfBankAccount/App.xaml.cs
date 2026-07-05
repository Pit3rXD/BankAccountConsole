using BankAccountCore;
using System.Net.Http;
using System.Windows;
using WpfBankAccount.Services;
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
            AccountDataService accountDataService = new AccountDataService();
            IAccountRepository accountRepository = new JsonAccountRepository();
            IAccountNumberGenerator accountNumberGenerator = new AccountNumberGenerator(accountDataService);
            BankAccountCore.IAuthService authService = new AuthService(accountRepository, accountNumberGenerator);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7065/");
            Interfaces.IApiService apiService = new ApiService(client);
            IViewModelFactory viewModelFactory = new ViewModelFactory(authService, apiService);
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
