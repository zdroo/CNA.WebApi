using MediatR;
using CNA.Application.Interfaces;

namespace CNA.Application.Catalog.CartOperations;

public static class ClearCart
{
    public record Command(Guid UserId) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId)
                        ?? throw new Exception("User not found");

            var cart = user.GetOrCreateCart();

            cart.Clear();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}