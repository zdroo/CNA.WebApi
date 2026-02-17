using CNA.Contracts.Models;
using MediatR;

namespace CNA.Application.Catalog.Commands.User
{
    public record LoginCommand(string Email, string Password)
        : IRequest<AuthResponse>;
}
