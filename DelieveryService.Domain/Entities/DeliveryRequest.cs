using DeliveryService.Domain.Enums;

namespace DeliveryService.Domain.Entities;

public class DeliveryRequest
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string? DeliveryInstructions { get; set; }

    public DeliveryStatus Status { get; set; } = DeliveryStatus.Pending;
}
