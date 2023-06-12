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
            CreateMap<Pagination, TransactionsPaginatedDto>()
                .ForMember(dest => dest.Pagination.ElementCount, opts => opts.MapFrom(src => src.ElementCount))
                .ForMember(dest => dest.Pagination.PageIndex, opts => opts.MapFrom(src => src.PageIndex))
                .ForMember(dest => dest.Pagination.PageCount, opts => opts.MapFrom(src => src.PageCount))
                .ForMember(dest => dest.Pagination.PageSize, opts => opts.MapFrom(src => src.PageSize));
        }
    }
}
