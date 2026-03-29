using AutoMapper;
using CNA.Application.Interfaces;
using CNA.Domain.Catalog.Entities;
using CNA.Domain.Catalog.ValueObjects;
using CNA.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CNA.Application.Catalog.CartOperations;

public static class CartCheckout
{
    public record Command(Guid UserId, Guid ShippingContactId, List<Guid> CartItemIds) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public Handler(
            IUserRepository userRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var saved = false;

            while (!saved)
            {
                try
                {
                    var user = await _userRepository.GetByIdAsync(command.UserId)
                        ?? throw new UserNotFoundException(command.UserId);

                    var shippingContact = user.ShippingContacts
                        .FirstOrDefault(sc => sc.Id == command.ShippingContactId)
                        ?? throw new ShippingContactNotFoundException(command.ShippingContactId, user.Id);

                    var snapshot = _mapper.Map<ShippingAddressSnapshot>(shippingContact);
                    var cart = user.GetOrCreateCart();
                    var itemsToCheckout = GetItemsToCheckout(cart, command.CartItemIds);
                    var itemsWithVariants = await GetItemsWithVariantsAsync(itemsToCheckout);

                    ValidateAndReserveStock(itemsWithVariants);

                    var order = CreateOrder(command, snapshot, itemsWithVariants);

                    foreach (var item in itemsWithVariants)
                        cart.RemoveItemByCartItemId(item.CartItem.Id);

                    await _orderRepository.AddAsync(order);
                    saved = true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                        await entry.ReloadAsync(cancellationToken);
                }
            }
        }

        private static List<CartItem> GetItemsToCheckout(Cart cart, List<Guid> cartItemIds)
        {
            var items = cart.Items
                .Where(x => cartItemIds.Contains(x.Id))
                .ToList();

            if (items.Count != cartItemIds.Count)
                throw new VariantsNotFoundException();

            return items;
        }

        private async Task<List<CartItemWithVariant>> GetItemsWithVariantsAsync(List<CartItem> items)
        {
            var variants = await _productRepository.GetByProductVariantIds(
                items.Select(x => x.ProductVariantId));

            var variantsDict = variants.ToDictionary(v => v.Id);

            if (variantsDict.Count != items.Count)
                throw new VariantsNotFoundException();

            return items.Select(item => new CartItemWithVariant(
                item,
                variantsDict[item.ProductVariantId]
            )).ToList();
        }

        private static void ValidateAndReserveStock(List<CartItemWithVariant> itemsWithVariants)
        {
            foreach (var item in itemsWithVariants)
                item.Variant.ValidateQuantity(item.CartItem.Quantity);

            foreach (var item in itemsWithVariants)
                item.Variant.DecreaseStock(item.CartItem.Quantity);
        }

        private static Order CreateOrder(
            Command command,
            ShippingAddressSnapshot snapshot,
            List<CartItemWithVariant> itemsWithVariants)
        {
            return new Order(
                command.UserId,
                command.ShippingContactId,
                snapshot,
                itemsWithVariants.Select(x =>
                    new OrderItem(
                        x.CartItem.ProductVariantId,
                        x.CartItem.Quantity,
                        x.CartItem.Price
                    )
                )
            );
        }

        private record CartItemWithVariant(CartItem CartItem, ProductVariant Variant);
    }
}