using CNA.Application.Catalog.Commands.Cart;
using CNA.Application.Interfaces;
using MediatR;

namespace CNA.Application.Catalog.CommandHandlers.Cart
{
    public class ClearCartCommandHandler : IRequestHandler<ClearCartCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClearCartCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ClearCartCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId)
                        ?? throw new Exception("User not found");

            var cart = user.GetOrCreateCart();

            cart.Clear();
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
