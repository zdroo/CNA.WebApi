using CNA.Application.Catalog.Commands.Order;
using CNA.Application.Interfaces;
using MediatR;

namespace CNA.Application.Catalog.CommandHandlers.Order
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelOrderCommandHandler(
            IOrderRepository orderRepository,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(CancelOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId)
                ?? throw new Exception("Order not found");

            order.Cancel();
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
