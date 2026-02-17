using CNA.Application.Catalog.Commands.User;
using CNA.Application.Interfaces;
using CNA.Contracts.Models;
using CNA.Domain.Catalog.Entities;
using MediatR;

namespace CNA.Application.Catalog.CommandHandlers
{
    public class RefreshTokenCommandHandler
        : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public RefreshTokenCommandHandler(
            IUserRepository userRepository,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByRefreshTokenAsync(request.Token)
                       ?? throw new UnauthorizedAccessException("Invalid refresh token.");

            var refreshToken = user.RefreshTokens
                .FirstOrDefault(t => t.Token == request.Token && t.IsActive)
                ?? throw new UnauthorizedAccessException("Expired or revoked token.");

            refreshToken.Revoke();
            var newRefreshToken = new RefreshToken(
                token: Guid.NewGuid().ToString(),
                expiresAt: DateTime.UtcNow.AddDays(7),
                userId: user.Id
            );
            user.AddRefreshToken(newRefreshToken);

            await _userRepository.UpdateAsync(user);

            var accessToken = _jwtService.GenerateToken(user);

            return new AuthResponse(accessToken);
        }
    }

}
