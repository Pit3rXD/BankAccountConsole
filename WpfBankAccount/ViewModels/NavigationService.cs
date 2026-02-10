using WpfBankAccount.Navigation;

namespace WpfBankAccount.ViewModels
{
    public class NavigationService : INavigationService
    {
        private readonly MainViewModel _mainViewModel;
        private readonly IViewModelFactory _factory;
        public NavigationService(MainViewModel mainViewModel, IViewModelFactory factory) 
        {
            _mainViewModel = mainViewModel;
            _factory = factory;
        }
        public void NavigateTo(ViewType viewType, object parameter)
        {
            object createNewViewModel = _factory.Create(viewType, parameter);
            _mainViewModel.CurrentView = createNewViewModel;
        }
    }
}