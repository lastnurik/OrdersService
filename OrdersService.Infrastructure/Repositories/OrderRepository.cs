using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using OrdersService.Domain.Entities;
using OrdersService.Domain.Repositories;
using OrdersService.Infrastructure.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OrdersService.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _db;

        public OrderRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Order order, CancellationToken ct = default)
        {
            await _db.AddAsync(order, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await GetByIdAsync(id, ct);
            if (entity != null)
            {
                _db.Orders.Remove(entity);
                await _db.SaveChangesAsync(ct);
                return true;
            }

            return false;
        }

        public async Task<(List<Order> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize, CancellationToken ct = default)
        {
            var query = _db.Orders.AsQueryable();
            var totalCount = await query.CountAsync(ct);
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, totalCount);
        }

        public async Task<Order?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _db.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id, ct);
        }

        public async Task UpdateAsync(Order order, CancellationToken ct = default)
        {
            _db.Orders.Update(order);
            await _db.SaveChangesAsync(ct);
        }
    }
}
