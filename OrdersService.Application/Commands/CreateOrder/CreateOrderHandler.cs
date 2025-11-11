using AutoMapper;
using MassTransit;
using MediatR;
using OrdersService.Application.IntegrationEvents;
using OrdersService.Domain.Entities;
using OrdersService.Domain.Repositories;
namespace OrdersService.Application.Commands.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _repo;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateOrderHandler(IOrderRepository repo, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repo = repo;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Order>(request);
            entity.Id = Guid.NewGuid();
            entity.Status = Domain.Enums.OrderStatus.Pending;
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.AddAsync(entity, cancellationToken);

            var orderCreatedEvent = _mapper.Map<OrderCreatedEvent>(entity);
            await _publishEndpoint.Publish(orderCreatedEvent, cancellationToken);
            return entity.Id;
        }
    }
}
