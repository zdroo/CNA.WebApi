using CNA.Application.Interfaces;
using CNA.Domain.Exceptions;
using MediatR;

namespace CNA.Application.Catalog.CartOperations;

public static class RemoveCartItem
{
    public record Command(Guid UserId, Guid CartItemId) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public Handler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId)
                ?? throw new UserNotFoundException(command.UserId);

            var cart = user.GetOrCreateCart();
            cart.RemoveItemByCartItemId(command.CartItemId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}