using BankAccountCore;
using WpfBankAccount.Navigation;

namespace WpfBankAccount.ViewModels
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly INavigationService _navigationService;
        private readonly IAuthService _authService;
        public ViewModelFactory(INavigationService navigationService, IAuthService authService)
        {
            _navigationService = navigationService;
            _authService = authService;
        }
        public object Create(ViewType viewType, object parameter)
        {
            switch (viewType)
            {
                case ViewType.Login:
                    return new LoginViewModel(_navigationService, _authService);

                case ViewType.Menu:
                    var account = parameter as BankAccount;
                    return new MenuViewModel(_navigationService, account);

                case ViewType.Deposit:
                    // Dodaj odpowiednią implementację lub zwróć null
                    return null;

                case ViewType.Withdraw:
                    // Dodaj odpowiednią implementację lub zwróć null
                    return null;

                case ViewType.History:
                    // Dodaj odpowiednią implementację lub zwróć null
                    return null;

                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}
