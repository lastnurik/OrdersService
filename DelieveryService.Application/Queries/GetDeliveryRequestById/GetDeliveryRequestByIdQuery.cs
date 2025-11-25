using DeliveryService.Application.DTOs;
using MediatR;

namespace DeliveryService.Application.Queries.GetDeliveryRequestById;

public record GetDeliveryRequestByIdQuery(Guid Id) : IRequest<DeliveryRequestDto?>;
