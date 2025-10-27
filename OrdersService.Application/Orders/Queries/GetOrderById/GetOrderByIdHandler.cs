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
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
    {
        private readonly IOrderRepository _repo;
        private readonly IMapper _mapper;

        public GetOrderByIdHandler(IOrderRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            var dto = _mapper.Map<OrderDto>(entity);
            return dto;
        }
    }
}
