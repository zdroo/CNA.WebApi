using CNA.Application.Interfaces;
using CNA.Application.Services;
using CNA.Contracts.Responses;
using CNA.Domain.Catalog.Entities;
using MediatR;

namespace CNA.Application.Catalog.AuthOperations;

public static class GoogleLogin
{
    public record Command(string IdToken) : IRequest<AuthResponse>;

    public class Handler : IRequestHandler<Command, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGoogleAuthService _googleAuthService;

        public Handler(
            IUserRepository userRepository,
            IJwtService jwtService,
            IUnitOfWork unitOfWork,
            IGoogleAuthService googleAuthService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
            _googleAuthService = googleAuthService;
        }

        public async Task<AuthResponse> Handle(Command request, CancellationToken cancellationToken)
        {
            var googleUser = await _googleAuthService.ValidateTokenAsync(request.IdToken);

            var user = await _userRepository.GetByGoogleIdAsync(googleUser.GoogleId)
                    ?? await _userRepository.GetByEmailAsync(googleUser.Email);

            if (user == null)
            {
                user = new User(
                    email: googleUser.Email,
                    googleId: googleUser.GoogleId,
                    firstName: googleUser.FirstName,
                    lastName: googleUser.LastName);

                await _userRepository.AddAsync(user);
            }

            var accessToken = _jwtService.GenerateToken(user);
            var refreshToken = new RefreshToken(
                token: Guid.NewGuid().ToString(),
                expiresAt: DateTime.UtcNow.AddDays(365),
                userId: user.Id);

            user.AddRefreshToken(refreshToken);
            await _userRepository.AddRefreshTokenAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new AuthResponse(accessToken, refreshToken.Token);
        }
    }
}
