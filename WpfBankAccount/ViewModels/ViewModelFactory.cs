using WpfBankAccount.Interfaces;
using WpfBankAccount.Navigation;
using WpfBankAccount.DTOs;

namespace WpfBankAccount.ViewModels
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IApiService _apiService;
        

        public ViewModelFactory(IApiService apiService)
        {
            _apiService = apiService;
        }

        public object Create(INavigationService navigationService, ViewType viewType, object parameter)
        {
            var account = parameter as DTOs.BankAccountDto;

            if (viewType == ViewType.Login)
            {
                return new LoginViewModel(navigationService, _apiService);
            }
            if(viewType ==  ViewType.Register)
            {
                return new RegisterViewModel(navigationService, _apiService);
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
                    return new DepositViewModel(navigationService, account, _apiService);

                case ViewType.Withdrawal:
                    return new WithdrawalViewModel(navigationService, account, _apiService);

                case ViewType.History:
                    return new HistoryViewModel(navigationService, account, _apiService);

                case ViewType.CheckBalance:
                    return new CheckBalanceViewModel(navigationService, account);

                case ViewType.Transfer:
                    return new TransferViewModel(navigationService, account, _apiService);

                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}
