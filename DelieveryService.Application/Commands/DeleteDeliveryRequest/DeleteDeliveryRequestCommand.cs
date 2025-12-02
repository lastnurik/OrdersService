using MediatR;

namespace DeliveryService.Application.Commands.DeleteDeliveryRequest;

public record DeleteDeliveryRequestCommand(Guid Id) : IRequest<bool>;
