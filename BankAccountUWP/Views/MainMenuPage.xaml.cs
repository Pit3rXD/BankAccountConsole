using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using BankAccountUWP.ViewModels;
using BankAccountLibrary.Models;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace BankAccountUWP.Views
{   
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class MainMenuPage : Page
    {
        private MainMenuViewModel ViewModel => (MainMenuViewModel)DataContext;
        public MainMenuPage()
        {
            this.InitializeComponent();

            var vm = new MainMenuViewModel();

            vm.LoginRequested += () => Frame.Navigate(typeof(LoginPage));
            vm.RegisterRequested += () => Frame.Navigate(typeof(RegisterPage));
            vm.ExitRequested += () => Application.Current.Exit();

            DataContext = vm;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.LoginCommand.Execute(null);
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RegisterCommand.Execute(null);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ExitCommand.Execute(null);
        }
    }
}
