using AutoMapper;
using BankAccountCore;
using BankApp.Api.DTOs;
using BankApp.Api.Models;

namespace BankApp.Api.Mappings
{
    public class TransactionMappingProfile : Profile
    {
        public TransactionMappingProfile() 
        {
            CreateMap<CreateTransactionDto, TransactionEntity>();
            CreateMap<TransactionEntity, TransactionDto>();
            CreateMap<BankAccountEntity, BankAccountCore.BankAccountDto>();
            CreateMap<BankAccountCore.BankAccountDto, BankAccountEntity>();
            CreateMap<Transaction, TransactionEntity>();
        }
    }
}
