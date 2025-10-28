using MediatR;
using OrdersService.Domain.Repositories;

namespace OrdersService.Application.Orders.Commands
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IOrderRepository _repo;

        public DeleteOrderHandler(IOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            return await _repo.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
