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

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] DeliveryService.Application.Commands.CreateDeliveryRequest.CreateDeliveryRequestCommand command, CancellationToken ct = default)
    {
        var id = await _mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] DeliveryService.Application.Commands.UpdateDeliveryRequest.UpdateDeliveryRequestCommand command, CancellationToken ct = default)
    {
        if (id != command.Id)
            return BadRequest();
        var result = await _mediator.Send(command, ct);
        return result ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new DeliveryService.Application.Commands.DeleteDeliveryRequest.DeleteDeliveryRequestCommand(id), ct);
        return result ? NoContent() : NotFound();
    }
}
