using AutoMapper;
using Bank.Interview.Application.Dtos;
using Bank.Interview.Domain.Entities;

namespace Bank.Interview.Application.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Transaction, TransactionDto>().ReverseMap();
        }
    }
}
