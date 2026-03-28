using CNA.Application.Interfaces;
using CNA.Application.Services;
using CNA.Contracts.Models;
using CNA.Domain.Catalog.Entities;
using MediatR;
using System.Security.Authentication;

namespace CNA.Application.Catalog.UserOperations;

public static class Login
{
    public record Command(string Email, string Password) : IRequest<AuthResponse>;

    public class Handler : IRequestHandler<Command, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtService jwtService,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResponse> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
                throw new AuthenticationException("Invalid email.");

            var isValid = _passwordHasher.Verify(request.Password, user.PasswordHash);

            if (!isValid)
                throw new AuthenticationException("Invalid password.");

            var token = _jwtService.GenerateToken(user);

            var refreshToken = new Domain.Catalog.Entities.RefreshToken(
                token: Guid.NewGuid().ToString(),
                expiresAt: DateTime.UtcNow.AddDays(365),
                userId: user.Id
            );

            user.AddRefreshToken(refreshToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new AuthResponse(token, refreshToken.Token);
        }
    }
}