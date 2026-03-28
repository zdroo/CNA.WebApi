using MediatR;
using CNA.Application.Interfaces;

namespace CNA.Application.Catalog.Orders;

public static class CancelOrder
{
    public record Command(Guid OrderId) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId)
                        ?? throw new Exception("Order not found");

            order.Cancel();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}