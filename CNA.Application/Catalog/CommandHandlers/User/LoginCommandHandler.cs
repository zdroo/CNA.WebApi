using CNA.Application.Catalog.Commands.User;
using CNA.Application.Interfaces;
using CNA.Application.Services;
using CNA.Contracts.Models;
using CNA.Domain.Catalog.Entities;
using MediatR;
using System.Security.Authentication;

namespace CNA.Application.Catalog.CommandHandlers.User
{
    public class LoginCommandHandler
        : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public LoginCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository
                .GetByEmailAsync(request.Email);

            if (user == null)
                throw new AuthenticationException("Invalid email.");

            var isValid = _passwordHasher
                .Verify(request.Password, user.PasswordHash);

            if (!isValid)
                throw new AuthenticationException("Invalid password.");

            var token = _jwtService.GenerateToken(user);

            var refreshToken = new RefreshToken(
                token: Guid.NewGuid().ToString(),
                expiresAt: DateTime.UtcNow.AddDays(365),
                userId: user.Id
            );

            user.AddRefreshToken(refreshToken);
            await _userRepository.UpdateAsync(user);

            return new AuthResponse(token);
        }
    }
}
