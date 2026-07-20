using AutoMapper;
using BankAccountCore;
using BankApp.Api.DTOs;
using BankApp.Api.Interfaces;
using BankApp.Api.Models;

namespace BankApp.Api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IBankAccountRepository _bankRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IBankAccountRepository bankRepository, ITransactionRepository transactionRepository,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            _bankRepository = bankRepository;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<TransactionDto> DepositAsync(decimal amount, int bankAccountId)
        {
            var account = await _bankRepository.GetByIdAsync(bankAccountId);
            if (account == null)
            {
                throw new ArgumentException($"Account with Id {bankAccountId} don't exists");
            }

            account.Balance += amount;
            await _bankRepository.UpdateAsync(account);

            var transactionEntity = new TransactionEntity
            {
                BankAccountId = bankAccountId,
                Amount = amount,
                BalanceAfter = account.Balance,
                Date = DateTime.UtcNow,
                Type = TransactionType.Deposit
            };

            await _transactionRepository.CreateAsync(transactionEntity);
            return _mapper.Map<TransactionDto>(transactionEntity);
        }

        public async Task<TransactionDto> WithdrawalAsync(decimal amount, int bankAccountId)
        {
            var account = await _bankRepository.GetByIdAsync(bankAccountId);
            if (account == null)
            {
                throw new ArgumentException($"Account with Id {bankAccountId} don't exists");
            }

            if (account.Balance < amount)
            {
                throw new BankApp.Api.Exceptions.InsufficientFundsException();
            }
            account.Balance -= amount;
            await _bankRepository.UpdateAsync(account);

            var transactionEntity = new TransactionEntity
            {
                BankAccountId = bankAccountId,
                Amount = amount,
                BalanceAfter = account.Balance,
                Date = DateTime.UtcNow,
                Type = TransactionType.Withdrawal
            };

            await _transactionRepository.CreateAsync(transactionEntity);
            return _mapper.Map<TransactionDto>(transactionEntity);
        }

        public async Task<IEnumerable<TransactionDto>> GetAllByAccountIdAsync(int accountId)
        {
            var transaction = await _transactionRepository.GetAllByAccountIdAsync(accountId);
            var getAll = _mapper.Map<IEnumerable<TransactionDto>>(transaction);

            return getAll;
        }

        public async Task<TransactionDto> TransferAsync(decimal amount, int bankAccountId, string recipientAccountNumber)
        {
            var sendersAccount = await _bankRepository.GetByIdAsync(bankAccountId);
            if (sendersAccount == null)
            {
                throw new ArgumentException($"Account with Id {bankAccountId} don't exists");
            }

            var recipientsAccount = await _bankRepository.GetByAccountNumberAsync(recipientAccountNumber);
            if (recipientsAccount == null)
            {
                throw new ArgumentException($"Recipients account with number: {recipientAccountNumber} don't exists");
            }
            if (sendersAccount.Balance < amount)
            {
                throw new BankApp.Api.Exceptions.InsufficientFundsException();
            }
            if (recipientsAccount.AccountNumber == sendersAccount.AccountNumber)
            {
                throw new ArgumentException($"You can not transfer to your self");
            }

            var result = await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var transferId = Guid.NewGuid();

                sendersAccount.Balance -= amount;
                recipientsAccount.Balance += amount;
                await _bankRepository.UpdateAsync(sendersAccount);
                await _bankRepository.UpdateAsync(recipientsAccount);

                var senderTransactionEntity = new TransactionEntity
                {
                    TransferId = transferId,
                    BankAccountId = bankAccountId,
                    Amount = amount,
                    Date = DateTime.UtcNow,
                    Type = TransactionType.TransferOut,
                    CounterpartyAccountNumber = recipientsAccount.AccountNumber,
                    BalanceAfter = sendersAccount.Balance
                };
                await _transactionRepository.CreateAsync(senderTransactionEntity);

                var recipientsTransferEntity = new TransactionEntity
                {
                    TransferId = transferId,
                    BankAccountId = recipientsAccount.Id,
                    Amount = amount,
                    Date = DateTime.UtcNow,
                    Type = TransactionType.TransferIn,
                    CounterpartyAccountNumber = sendersAccount.AccountNumber,
                    BalanceAfter = recipientsAccount.Balance
                };
                await _transactionRepository.CreateAsync(recipientsTransferEntity);

                return _mapper.Map<TransactionDto>(senderTransactionEntity);
            });

            return result;
        }
    }
}
