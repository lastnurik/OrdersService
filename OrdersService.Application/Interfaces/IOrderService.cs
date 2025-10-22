using OrdersService.Application.DTOs;
using OrdersService.Application.Orders.Commands;
using OrdersService.Application.Orders.Queries;

namespace OrdersService.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto?> GetByIdAsync(GetOrderByIdQuery query, CancellationToken ct = default);
        Task<PaginatedResult<OrderDto>> GetAllAsync(GetAllOrdersQuery query, CancellationToken ct = default);
        Task<Guid> CreateAsync(CreateOrderCommand command, CancellationToken ct = default);
        Task<bool> UpdateAsync(UpdateOrderCommand command, CancellationToken ct = default);
        Task<bool> DeleteAsync(DeleteOrderCommand command, CancellationToken ct = default);
    }
}
