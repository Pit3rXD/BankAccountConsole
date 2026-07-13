using AutoMapper;
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
        }
    }
}
