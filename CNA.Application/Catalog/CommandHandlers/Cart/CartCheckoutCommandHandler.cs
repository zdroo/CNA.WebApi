using CNA.Application.Catalog.Commands.Cart;
using CNA.Application.Interfaces;
using CNA.Domain.Catalog.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CNA.Application.Catalog.CommandHandlers.Cart
{
    public class CartCheckoutCommandHandler : IRequestHandler<CartCheckoutCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IOrderRepository _orderRepository;

        public CartCheckoutCommandHandler(
            IUserRepository userRepository,
            IProductVariantRepository productVariantRepository,
            IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _productVariantRepository = productVariantRepository;
            _orderRepository = orderRepository;
        }

        public async Task Handle(CartCheckoutCommand command, CancellationToken cancellationToken)
        {
            var saved = false;

            while (!saved)
            {
                try
                {
                    var user = await _userRepository.GetByIdAsync(command.UserId)
                        ?? throw new Exception("User not found");

                    var cart = user.GetOrCreateCart();

                    var itemsToCheckout = cart.Items
                        .Where(x => command.CartItemIds.Contains(x.Id))
                        .ToList();

                    if (itemsToCheckout.Count != command.CartItemIds.Count)
                        throw new Exception("No match between cart items and items to checkout");

                    var variants = await _productVariantRepository.GetByProductVariantIds(
                        itemsToCheckout.Select(x => x.ProductVariantId));

                    if (variants.Count() != itemsToCheckout.Count)
                        throw new Exception("Some product variants were not found");

                    var variantsDict = variants.ToDictionary(v => v.Id);

                    var itemsWithVariants = itemsToCheckout.Select(item => new
                    {
                        CartItem = item,
                        Variant = variantsDict[item.ProductVariantId]
                    }).ToList();

                    foreach (var itemVariant in itemsWithVariants)
                        itemVariant.Variant.ValidateQuantity(itemVariant.CartItem.Quantity);

                    foreach (var itemVariant in itemsWithVariants)
                        itemVariant.Variant.DecreaseStock(itemVariant.CartItem.Quantity);

                    var order = new Domain.Catalog.Entities.Order(
                        command.UserId,
                        itemsWithVariants.Select(x =>
                            new OrderItem(
                                x.CartItem.ProductVariantId,
                                x.CartItem.Quantity,
                                x.CartItem.Price
                            )
                        )
                    );

                    foreach (var x in itemsWithVariants)
                        cart.RemoveItemByCartItemId(x.CartItem.Id);

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
    }
}
