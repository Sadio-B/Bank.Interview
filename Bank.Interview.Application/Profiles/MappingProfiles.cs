using AutoMapper;
using Bank.Interview.Application.Common;
using Bank.Interview.Application.Dtos;
using Bank.Interview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
