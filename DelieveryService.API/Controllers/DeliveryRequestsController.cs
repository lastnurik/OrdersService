using DeliveryService.Application.Queries.GetDeliveryRequestById;
using DeliveryService.Application.Queries.GetDeliveryRequests;
using DeliveryService.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeliveryRequestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeliveryRequestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DeliveryRequestDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    {
        var (items, total) = await _mediator.Send(new GetDeliveryRequestsQuery(pageNumber, pageSize), ct);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DeliveryRequestDto>> GetById(Guid id, CancellationToken ct = default)
    {
        var dto = await _mediator.Send(new GetDeliveryRequestByIdQuery(id), ct);
        return dto is null ? NotFound() : Ok(dto);
    }
}
