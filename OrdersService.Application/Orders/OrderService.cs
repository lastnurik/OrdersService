using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrdersService.Application.DTOs;
using OrdersService.Application.Interfaces;
using OrdersService.Application.Orders.Commands;
using OrdersService.Application.Orders.Queries;
using OrdersService.Domain.Entities;
using OrdersService.Domain.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OrdersService.Application.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(CreateOrderCommand command, CancellationToken ct = default)
        {
            var entity = _mapper.Map<Order>(command);
            entity.Id = Guid.NewGuid();
            entity.Status = Domain.Enums.OrderStatus.Pending;
            entity.CreatedAt = DateTime.UtcNow;

            await _repo.AddAsync(entity);
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(DeleteOrderCommand command, CancellationToken ct = default)
        {
            return await _repo.DeleteAsync(command.Id, ct);
        }

        public async Task<PaginatedResult<OrderDto>> GetAllAsync(GetAllOrdersQuery query, CancellationToken ct = default)
        {
            var (pagedItems, totalCount) = await _repo.GetAllAsync(query.PageNumber, query.PageSize, ct);
            var totalPages = (int)Math.Ceiling((double)totalCount / query.PageSize);

            var orderDtos = _mapper.Map<List<OrderDto>>(pagedItems);

            return new PaginatedResult<OrderDto>
            {
                Items = orderDtos,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
            };
        }

        public async Task<OrderDto?> GetByIdAsync(GetOrderByIdQuery query, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(query.Id, ct);
            var dto = _mapper.Map<OrderDto>(entity);
            return dto;
        }

        public async Task<bool> UpdateAsync(UpdateOrderCommand command, CancellationToken ct = default)
        {
            var existingEntity = await _repo.GetByIdAsync(command.Id, ct);

            if (existingEntity == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(command.CustomerName))
            {
                existingEntity.CustomerName = command.CustomerName;
            }

            existingEntity.TotalAmount = command.TotalAmount;

            if (!string.IsNullOrEmpty(command.Status))
            {
                if (Enum.TryParse<Domain.Enums.OrderStatus>(command.Status, true, out var status))
                {
                    existingEntity.Status = status;
                }
                else
                {
                    throw new Exceptions.InvalidOrderStatusException(command.Status);
                }
            }

            if (!string.IsNullOrEmpty(command.Description))
            {
                existingEntity.Description = command.Description;
            }

            await _repo.UpdateAsync(existingEntity);
            return true;
        }
    }
}
