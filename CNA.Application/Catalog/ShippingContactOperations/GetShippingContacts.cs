using AutoMapper;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.ShippingContactOperations;

public static class GetShippingContacts
{
    public record Query(Guid UserId) : IRequest<List<ShippingContactResponse>>;

    public class Handler : IRequestHandler<Query, List<ShippingContactResponse>>
    {
        private readonly IShippingContactRepository _repo;
        private readonly IMapper _mapper;

        public Handler(IShippingContactRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ShippingContactResponse>> Handle(Query query, CancellationToken cancellationToken)
        {
            var contacts = await _repo.GetByUserIdAsync(query.UserId);
            return _mapper.Map<List<ShippingContactResponse>>(contacts);
        }
    }
}
