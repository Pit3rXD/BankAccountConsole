using BankApp.Api.Data;
using BankApp.Api.Interfaces;

namespace BankApp.Api.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BankAppDbContext _context;

        public UnitOfWork(BankAppDbContext context)
        {
            _context = context;
        }
        public async Task<TResult> ExecuteInTransactionAsync<TResult>(Func<Task<TResult>> operation)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                TResult result = await operation();
                await transaction.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
