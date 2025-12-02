using DeliveryService.Domain.Repositories;
using MediatR;

namespace DeliveryService.Application.Commands.DeleteDeliveryRequest;

public class DeleteDeliveryRequestHandler : IRequestHandler<DeleteDeliveryRequestCommand, bool>
{
    private readonly IDeliveryRequestRepository _repo;

    public DeleteDeliveryRequestHandler(IDeliveryRequestRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(DeleteDeliveryRequestCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
            return false;
        await _repo.DeleteAsync(entity, cancellationToken);
        return true;
    }
}
