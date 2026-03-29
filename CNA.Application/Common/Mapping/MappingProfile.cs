using AutoMapper;
using CNA.Contracts.Responses;
using CNA.Domain.Catalog.Entities;
using CNA.Domain.Catalog.Entities.Localization;
using CNA.Domain.Catalog.ValueObjects;

namespace CNA.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<List<VariantAttribute>, VariantFiltersResponse>()
                .ConstructUsing(attrs => new VariantFiltersResponse
                {
                    Attributes = attrs
                        .GroupBy(a => a.Name)
                        .Select(g => new AttributeFilter
                        {
                            Name = g.Key,
                            Values = g.Select(a => a.Value).Distinct().ToList()
                        })
                        .ToList()
                });

            CreateMap<CartItem, CartItemResponse>();
            //.ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total));

            CreateMap<Cart, CartResponse>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.GetTotal()));

            CreateMap<Category, CategoryResponse>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id));

            CreateMap<Country, CountryResponse>();

            CreateMap<OrderItem, OrderItemResponse>()
                .ForMember(dest => dest.ProductVariantId, opt => opt.MapFrom(src => src.ProductVariantId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total));

            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<ProductVariant, ProductVariantResponse>()
                .ForMember(dest => dest.ProductVariantId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.Stock.Quantity))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src =>
                    src.Attributes.ToDictionary(a => a.Name, a => a.Value)
                ))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Product.CategoryId));

            CreateMap<Product, ProductResponse>();
            //.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            //.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            //.ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            //.ForMember(dest => dest.Variants, opt => opt.MapFrom(src => src.Variants));

            CreateMap<ShippingContact, ShippingAddressSnapshot>()
                .ForMember(dest => dest.AddressLine1, opt => opt.MapFrom(src => src.Address.AddressLine1))
                .ForMember(dest => dest.AddressLine2, opt => opt.MapFrom(src => src.Address.AddressLine2))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Address.Region))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.Address.CountryCode));
        }
    }
}
