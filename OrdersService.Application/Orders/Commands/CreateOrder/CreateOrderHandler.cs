using AutoMapper;
using MediatR;
using OrdersService.Domain.Entities;
using OrdersService.Domain.Repositories;
namespace OrdersService.Application.Orders.Commands
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _repo;
        private readonly IMapper _mapper;

        public CreateOrderHandler(IOrderRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Order>(request);
            entity.Id = Guid.NewGuid();
            entity.Status = Domain.Enums.OrderStatus.Pending;
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.AddAsync(entity, cancellationToken);
            return entity.Id;
        }
    }
}
