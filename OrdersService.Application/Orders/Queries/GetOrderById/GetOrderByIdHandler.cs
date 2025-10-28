using AutoMapper;
using MediatR;
using OrdersService.Application.DTOs;
using OrdersService.Domain.Repositories;

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
