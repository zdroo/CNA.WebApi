using CNA.Application.Interfaces;
using CNA.Contracts.Models;
using MediatR;

namespace CNA.Application.Catalog.AuthOperations;

public static class RefreshToken
{
    public record Command(string Token) : IRequest<AuthResponse>;

    public class Handler : IRequestHandler<Command, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserRepository userRepository, IJwtService jwtService, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResponse> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByRefreshTokenAsync(request.Token)
                       ?? throw new UnauthorizedAccessException("Invalid refresh token.");

            var refreshToken = user.RefreshTokens
                .FirstOrDefault(t => t.Token == request.Token && t.IsActive)
                ?? throw new UnauthorizedAccessException("Expired or revoked token.");

            refreshToken.Revoke();

            var newRefreshToken = new Domain.Catalog.Entities.RefreshToken(
                token: Guid.NewGuid().ToString(),
                expiresAt: DateTime.UtcNow.AddDays(365),
                userId: user.Id
            );

            user.AddRefreshToken(newRefreshToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var accessToken = _jwtService.GenerateToken(user);

            return new AuthResponse(accessToken, newRefreshToken.Token);
        }
    }
}