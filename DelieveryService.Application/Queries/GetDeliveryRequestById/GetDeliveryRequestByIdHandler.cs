using AutoMapper;
using DeliveryService.Application.DTOs;
using DeliveryService.Domain.Repositories;
using MediatR;

namespace DeliveryService.Application.Queries.GetDeliveryRequestById;

public class GetDeliveryRequestByIdHandler : IRequestHandler<GetDeliveryRequestByIdQuery, DeliveryRequestDto?>
{
    private readonly IDeliveryRequestRepository _repo;
    private readonly IMapper _mapper;

    public GetDeliveryRequestByIdHandler(IDeliveryRequestRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<DeliveryRequestDto?> Handle(GetDeliveryRequestByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
        return entity is null ? null : _mapper.Map<DeliveryRequestDto>(entity);
    }
}
