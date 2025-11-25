using DeliveryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Infrastructure.Data.Configurations
{
    public class DeliveryRequestConfiguration : IEntityTypeConfiguration<DeliveryRequest>
    {
        public void Configure(EntityTypeBuilder<DeliveryRequest> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.OrderId)
                .IsUnique();

            builder.Property(x => x.CustomerName)
                .HasMaxLength(200);

            builder.Property(x => x.Street)
                .HasMaxLength(300);

            builder.Property(x => x.City)
                .HasMaxLength(100);

            builder.Property(x => x.PostalCode)
                .HasMaxLength(20);

            builder.Property(x => x.Country)
                .HasMaxLength(100);

            builder.Property(e => e.Status)
                .IsRequired()
                .HasConversion<string>();
        }
    }
}
