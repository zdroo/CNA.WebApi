using CNA.Application.Catalog.Commands.Cart;
using CNA.Application.Interfaces;
using MediatR;

namespace CNA.Application.Catalog.CommandHandlers.Cart
{
    public class RemoveCartItemCommandHandler : IRequestHandler<RemoveCartItemCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public RemoveCartItemCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task Handle(RemoveCartItemCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId)
                ?? throw new Exception("User not found");

            var cart = user.GetOrCreateCart();

            cart.RemoveItemByCartItemId(command.CartItemId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
