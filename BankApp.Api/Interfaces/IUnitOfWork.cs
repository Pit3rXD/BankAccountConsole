
namespace BankApp.Api.Interfaces
{
    public interface IUnitOfWork
    {
        public Task<TResult> ExecuteInTransactionAsync<TResult>(Func<Task<TResult>> operation);
    }
}
