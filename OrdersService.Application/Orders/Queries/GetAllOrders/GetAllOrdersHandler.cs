using AutoMapper;
using MediatR;
using OrdersService.Application.DTOs;
using OrdersService.Application.Orders.Queries;
using OrdersService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OrdersService.Application.Orders.Queries
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, PaginatedResult<OrderDto>>
    {
        private readonly IOrderRepository _repo;
        private readonly IMapper _mapper;

        public GetAllOrdersHandler(IOrderRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var (pagedItems, totalCount) = await _repo.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
            var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

            var orderDtos = _mapper.Map<List<OrderDto>>(pagedItems);

            return new PaginatedResult<OrderDto>
            {
                Items = orderDtos,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
            };
        }
    }
}
