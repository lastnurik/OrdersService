using Microsoft.AspNetCore.Mvc;
using OrdersService.Application.DTOs;
using OrdersService.API.Models.Requests;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using OrdersService.Application.Queries.GetOrderById;
using OrdersService.Application.Queries.GetAllOrders;
using OrdersService.Application.Commands.CreateOrder;
using OrdersService.Application.Commands.UpdateOrder;
using OrdersService.Application.Commands.DeleteOrder;

namespace OrdersService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IMediator mediator, ILogger<OrdersController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<OrderDto>>> GetAll([FromQuery] GetAllOrdersRequest request, CancellationToken ct)
        {
            _logger.LogInformation("Getting all orders");
            var query = new GetAllOrdersQuery(request.PageNumber, request.PageSize);
            var orders = await _mediator.Send(query, ct);
            _logger.LogInformation("Returned orders");
            return Ok(orders);
        }

        [HttpGet("{id:guid}", Name = "GetOrderById")]
        public async Task<ActionResult<OrderDto>> Get(Guid id, CancellationToken ct)
        {
            _logger.LogInformation("Getting an order with id: {OrderId}", id);
            var order = await _mediator.Send(new GetOrderByIdQuery(id), ct);
            if (order == null)
            {
                _logger.LogInformation("Order with Id=\"{OrderId}\" was Not Found", id);
                return NotFound();
            }

            _logger.LogInformation("Returned an order with id: {OrderId}", id);
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request, CancellationToken ct)
        {
            _logger.LogInformation("Creating an order");
            var command = new CreateOrderCommand(request.CustomerName, request.TotalAmount, request.Description ?? string.Empty);
            var id = await _mediator.Send(command, ct);
            _logger.LogInformation("Order with Id=\"{OrderId}\" was created", id);
            return StatusCode(StatusCodes.Status201Created, new { id });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOrderRequest request, CancellationToken ct)
        {
            _logger.LogInformation("Updating an order with Id: {OrderId}", id);
            var command = new UpdateOrderCommand(id, request.CustomerName ?? string.Empty, request.TotalAmount, request.Status, request.Description ?? string.Empty);
            var ok = await _mediator.Send(command, ct);

            if (ok)
            {
                _logger.LogInformation("Order with Id=\"{OrderId}\" was updated", id);
                return NoContent();
            }

            _logger.LogInformation("Order with Id=\"{OrderId}\" was Not Found", id);
            return NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            _logger.LogInformation("Deleting an order with Id: {OrderId}", id);
            var ok = await _mediator.Send(new DeleteOrderCommand(id), ct);

            if (ok)
            {
                _logger.LogInformation("Order with Id=\"{OrderId}\" was deleted", id);
                return NoContent();
            }

            _logger.LogInformation("Order with Id=\"{OrderId}\" was Not Found", id);
            return NotFound();
        }
    }
}
