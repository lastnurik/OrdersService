using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Repositories;
using DeliveryService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Infrastructure.Repositories;

public class DeliveryRequestRepository : IDeliveryRequestRepository
{
    private readonly DeliveryDbContext _db;

    public DeliveryRequestRepository(DeliveryDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(DeliveryRequest request, CancellationToken ct)
    {
        await _db.AddAsync(request, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await GetByIdAsync(id, ct);
        if (entity != null)
        {
            _db.DeliveryRequests.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
        return false;
    }

    public async Task<(List<DeliveryRequest> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize, CancellationToken ct)
    {
        var query = _db.DeliveryRequests.AsQueryable();
        var total = await query.CountAsync(ct);
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(ct);
        return (items, total);
    }

    public async Task<DeliveryRequest?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _db.DeliveryRequests.FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task UpdateAsync(DeliveryRequest request, CancellationToken ct)
    {
        _db.DeliveryRequests.Update(request);
        await _db.SaveChangesAsync(ct);
    }

    public Task<bool> ExistsForOrderAsync(Guid orderId, CancellationToken ct)
    {
        return _db.DeliveryRequests.AnyAsync(x => x.OrderId == orderId, ct);
    }
}
