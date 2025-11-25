using DeliveryService.Application.DTOs;
using MediatR;

namespace DeliveryService.Application.Queries.GetDeliveryRequests;

public record GetDeliveryRequestsQuery(int PageNumber = 1, int PageSize = 20) : IRequest<(IReadOnlyList<DeliveryRequestDto> Items, int TotalCount)>;
