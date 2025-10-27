using AutoMapper;
using MediatR;
using OrdersService.Application.Orders.Commands;
using OrdersService.Domain.Entities;
using OrdersService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

            await _repo.AddAsync(entity, cancellationToken);
            return entity.Id;
        }
    }
}
