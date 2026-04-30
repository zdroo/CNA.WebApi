using CNA.Application.Interfaces;
using CNA.Domain.Catalog.Entities.Localization;
using MediatR;

namespace CNA.Application.Catalog.ShippingContactOperations;

public static class AddShippingContact
{
    public record Command(
        Guid UserId,
        string FullName,
        string PhoneNumber,
        string AddressLine1,
        string AddressLine2,
        string City,
        string Region,
        string PostalCode,
        string CountryCode
    ) : IRequest<Guid>;

    public class Handler : IRequestHandler<Command, Guid>
    {
        private readonly IShippingContactRepository _repo;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IShippingContactRepository repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(Command cmd, CancellationToken cancellationToken)
        {
            var address = new Address
            {
                UserId = cmd.UserId,
                AddressLine1 = cmd.AddressLine1,
                AddressLine2 = cmd.AddressLine2,
                City = cmd.City,
                Region = cmd.Region,
                PostalCode = cmd.PostalCode,
                CountryCode = cmd.CountryCode,
            };

            var contact = new ShippingContact
            {
                UserId = cmd.UserId,
                FullName = cmd.FullName,
                PhoneNumber = cmd.PhoneNumber,
                AddressId = address.Id,
                Address = address,
            };

            await _repo.AddAsync(contact);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return contact.Id;
        }
    }
}
