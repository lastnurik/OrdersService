using AutoMapper;
using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Repositories;
using MediatR;

namespace DeliveryService.Application.Commands.UpdateDeliveryRequest;

public class UpdateDeliveryRequestHandler : IRequestHandler<UpdateDeliveryRequestCommand, bool>
{
    private readonly IDeliveryRequestRepository _repo;
    private readonly IMapper _mapper;

    public UpdateDeliveryRequestHandler(IDeliveryRequestRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateDeliveryRequestCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
            return false;

        _mapper.Map(request, entity);
        entity.UpdatedAt = DateTime.UtcNow;
        await _repo.UpdateAsync(entity, cancellationToken);
        return true;
    }
}
