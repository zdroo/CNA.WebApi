using CNA.Contracts.Responses;
using MediatR;
using System.Net;

namespace CNA.Application.Catalog.Queries
{
    public record GetProductByIdQuery(Guid ProductId) : IRequest<ProductResponse?>;

}
