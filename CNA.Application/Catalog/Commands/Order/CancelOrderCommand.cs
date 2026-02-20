using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.Commands.Order
{
    public record CancelOrderCommand(Guid OrderId) : IRequest;
}
