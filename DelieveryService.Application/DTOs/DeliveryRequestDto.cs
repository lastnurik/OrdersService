using DeliveryService.Domain.Enums;

namespace DeliveryService.Application.DTOs;

public record DeliveryRequestDto(
    Guid Id,
    Guid OrderId,
    string CustomerName,
    decimal TotalAmount,
    string Description,
    DateTime CreatedAt,
    string Street,
    string City,
    string PostalCode,
    string Country,
    string? DeliveryInstructions,
    DeliveryStatus Status
);
