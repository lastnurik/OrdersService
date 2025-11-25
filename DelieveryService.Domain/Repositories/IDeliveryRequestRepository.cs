using DeliveryService.Domain.Entities;

namespace DeliveryService.Domain.Repositories;

public interface IDeliveryRequestRepository
{
    Task<DeliveryRequest?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<(List<DeliveryRequest> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize, CancellationToken ct);
    Task AddAsync(DeliveryRequest request, CancellationToken ct);
    Task UpdateAsync(DeliveryRequest request, CancellationToken ct);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct);
    Task<bool> ExistsForOrderAsync(Guid orderId, CancellationToken ct);
}
