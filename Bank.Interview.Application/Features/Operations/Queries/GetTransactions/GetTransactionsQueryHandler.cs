﻿using AutoMapper;
using Bank.Interview.Application.Contrats;
using Bank.Interview.Application.Dtos;
using Bank.Interview.Application.Extensions;
using MediatR;

namespace Bank.Interview.Application.Features.Operations.Queries.GetTransactions
{
    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, TransactionsPaginatedDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTransactionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TransactionsPaginatedDto> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactionsCount = await _unitOfWork.TransactionRepository.CountAsync();
            var transactions = await _unitOfWork.TransactionRepository.GetTransactionsPaginatedByAccountId(request.AccountId, request.PaginationRequest);

            TransactionsPaginatedDto transactionsPaginated = new()
            {
                Transactions = _mapper.Map<IEnumerable<TransactionDto>>(transactions),
                Pagination = request.PaginationRequest.CreatePagination(transactionsCount),
            };

            return transactionsPaginated;
        }
    }
}
