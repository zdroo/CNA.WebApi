using CNA.Contracts.Models;
using MediatR;

namespace CNA.Application.Catalog.Commands.User
{
    public record RefreshTokenCommand(string Token) : IRequest<AuthResponse>;
}
