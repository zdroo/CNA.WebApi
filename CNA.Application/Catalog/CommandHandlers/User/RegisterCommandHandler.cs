using CNA.Application.Catalog.Commands.User;
using CNA.Application.Interfaces;
using CNA.Application.Services;
using CNA.Contracts.Models;
using MediatR;

namespace CNA.Application.Catalog.CommandHandlers.User
{
    public class RegisterCommandHandler
        : IRequestHandler<RegisterCommand, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public RegisterCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository
                .GetByEmailAsync(request.Email);

            if (existingUser != null)
                throw new Exception("Email already registered.");

            var passwordHash = _passwordHasher.Hash(request.Password);

            var user = new Domain.Catalog.Entities.User(request.Email, passwordHash);

            await _userRepository.AddAsync(user);

            var token = _jwtService.GenerateToken(user);

            return new AuthResponse(token);
        }
    }

}
