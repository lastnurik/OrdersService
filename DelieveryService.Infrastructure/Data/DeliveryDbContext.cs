using DeliveryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Infrastructure.Data;

public class DeliveryDbContext : DbContext
{
    public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options) : base(options) { }

    public DbSet<DeliveryRequest> DeliveryRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeliveryDbContext).Assembly);
    }
}
