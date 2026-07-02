using WpfBankAccount.DTOs;
using System.Windows.Input;
using WpfBankAccount.Navigation;

namespace WpfBankAccount.ViewModels
{
    public class CheckBalanceViewModel 
    {
        private readonly INavigationService _navigationService;
        private readonly BankAccountDto _dto;

        public string OwnerName => _dto.OwnerName;
        public string AccountNumber => _dto.AccountNumber;
        public decimal Balance => _dto.Balance;
        public ICommand BackCommand { get; }
                            
        public CheckBalanceViewModel(INavigationService navigationService, BankAccountDto dto)
        {
            _navigationService = navigationService;
            _dto = dto;

            BackCommand = new RelayCommand(Back, CanBack);
        }
        private void Back(object parameter)
        {
            _navigationService.NavigateTo(ViewType.Menu, _dto);
        }
        private bool CanBack(object parameter)
        {
            return true;
        }
    }
}
