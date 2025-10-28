using OrdersService.Domain.Entities;

namespace OrdersService.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<(List<Order> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize, CancellationToken ct);
        Task AddAsync(Order order, CancellationToken ct);
        Task UpdateAsync(Order order, CancellationToken ct);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct);
    }
}
