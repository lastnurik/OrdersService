using AutoMapper;
using DeliveryService.Application.DTOs;
using DeliveryService.Domain.Repositories;
using MediatR;

namespace DeliveryService.Application.Queries.GetDeliveryRequests;

public class GetDeliveryRequestsHandler : IRequestHandler<GetDeliveryRequestsQuery, (IReadOnlyList<DeliveryRequestDto> Items, int TotalCount)>
{
    private readonly IDeliveryRequestRepository _repo;
    private readonly IMapper _mapper;

    public GetDeliveryRequestsHandler(IDeliveryRequestRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<(IReadOnlyList<DeliveryRequestDto> Items, int TotalCount)> Handle(GetDeliveryRequestsQuery request, CancellationToken cancellationToken)
    {
        var (items, total) = await _repo.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        return (_mapper.Map<IReadOnlyList<DeliveryRequestDto>>(items), total);
    }
}
