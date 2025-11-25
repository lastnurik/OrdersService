using AutoMapper;
using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Repositories;
using MediatR;

namespace DeliveryService.Application.Commands.CreateDeliveryRequest;

public class CreateDeliveryRequestHandler : IRequestHandler<CreateDeliveryRequestCommand, Guid>
{
    private readonly IDeliveryRequestRepository _repo;
    private readonly IMapper _mapper;

    public CreateDeliveryRequestHandler(IDeliveryRequestRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateDeliveryRequestCommand request, CancellationToken cancellationToken)
    {
        if (await _repo.ExistsForOrderAsync(request.OrderId, cancellationToken))
        {
            throw new InvalidOperationException("DeliveryRequest already exists for this order");
        }

        var entity = _mapper.Map<DeliveryRequest>(request);
        entity.Id = Guid.NewGuid();
        entity.UpdatedAt = DateTime.UtcNow;

        await _repo.AddAsync(entity, cancellationToken);
        return entity.Id;
    }
}
