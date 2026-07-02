using BankAccountCore;
using WpfBankAccount.Interfaces;
using WpfBankAccount.Navigation;

namespace WpfBankAccount.ViewModels
{
    public class ViewModelFactory : IViewModelFactory
    {
        //private readonly BankAccountCore.IApiService _authService;
        private readonly Interfaces.IApiService _apiService;
        private readonly TransactionService _transactionService;
        

        public ViewModelFactory(BankAccountCore.IApiService authService, Interfaces.IApiService apiService)
        {
            _authService = authService;
            _apiService = apiService;
            _transactionService = new TransactionService();
        }

        public object Create(INavigationService navigationService, ViewType viewType, object parameter)
        {
            var account = parameter as BankAccountDto;

            if (viewType == ViewType.Login)
            {
                return new LoginViewModel(navigationService, _apiService);
            }
            if(viewType ==  ViewType.Register)
            {
                return new RegisterViewModel(navigationService, _authService);
            }
            if(account == null)
            {
                throw new ArgumentNullException(nameof(parameter), "This view requires a BankAccount");
            }
            switch (viewType)
            {
                case ViewType.Menu:
                    return new MenuViewModel(navigationService, account);

                case ViewType.Deposit:
                    return new DepositViewModel(navigationService, _authService, account, _transactionService);

                case ViewType.Withdrawal:
                    return new WithdrawalViewModel(navigationService, _authService, account, _transactionService);

                case ViewType.History:
                    return new HistoryViewModel(navigationService, account);

                case ViewType.CheckBalance:
                    return new CheckBalanceViewModel(navigationService, account);

                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}
