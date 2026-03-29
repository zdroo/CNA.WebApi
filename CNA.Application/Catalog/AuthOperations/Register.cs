using CNA.Application.Interfaces;
using CNA.Application.Services;
using CNA.Contracts.Responses;
using CNA.Domain.Catalog.Entities;
using CNA.Domain.Exceptions;
using MediatR;

namespace CNA.Application.Catalog.AuthOperations;

public static class Register
{
    public record Command(string Email, string Password) : IRequest<AuthResponse>;

    public class Handler : IRequestHandler<Command, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public Handler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> Handle(Command request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);

            if (existingUser != null)
                throw new EmailAlreadyRegisteredException($"Email already registered '{request.Email}'.");

            var passwordHash = _passwordHasher.Hash(request.Password);

            var user = new User(request.Email, passwordHash);

            var token = _jwtService.GenerateToken(user);
            var refreshToken = new RefreshToken(
                token: Guid.NewGuid().ToString(),
                expiresAt: DateTime.UtcNow.AddDays(365),
                userId: user.Id
            );

            user.AddRefreshToken(refreshToken);
            await _userRepository.AddAsync(user);

            return new AuthResponse(token, refreshToken.Token);
        }
    }
}