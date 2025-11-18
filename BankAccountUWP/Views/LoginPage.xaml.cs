using BankAccountLibrary.Services;
using BankAccountUWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BankAccountUWP.Views
{
    public sealed partial class LoginPage : Page
    {
        private readonly LoginPageViewModel vm;

        public LoginPage()
        {
            InitializeComponent();
            var authService = new AuthService();
            vm = new LoginPageViewModel(authService);
            DataContext = vm;
            PasswordBox.PasswordChanged += PasswordBox_PasswordChanged;
            LoginButton.Click += (s, e) => vm.LoginClicked.Execute(null);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
            else
            {
                Frame.Navigate(typeof(MainMenuPage));
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            vm.Password = PasswordBox.Password;
        }
    }
}