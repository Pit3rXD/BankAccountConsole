using WpfBankAccount.Navigation;

namespace WpfBankAccount.ViewModels
{
    public interface IViewModelFactory
    {
        object Create(ViewType viewType, object parameter = null);
    }
}
