using AutoMapper;
using CNA.Application.Catalog.ProductVariantOperations;
using CNA.Contracts.Responses;
using CNA.Domain.Catalog.Entities;
using CNA.Domain.Catalog.Entities.Localization;
using CNA.Domain.Catalog.ValueObjects;
using CNA.Domain.Filters;

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

            CreateMap<Category, CategoryResponse>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

            CreateMap<Country, CountryResponse>();

            CreateMap<OrderItem, OrderItemResponse>();

            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id));

            CreateMap<ProductVariant, ProductVariantResponse>()
                .ForMember(dest => dest.VariantId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.Stock.Quantity))
                .ForMember(dest => dest.VariantSlug, opt => opt.MapFrom(src => src.Slug))
                .ForMember(dest => dest.ProductSlug, opt => opt.MapFrom(src => src.Product.Slug))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src =>
                    src.Attributes.ToDictionary(a => a.Name, a => a.Value)
                ))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Product.CategoryId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Product.Category.Name))
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images.OrderBy(i => i.SortOrder).Select(i => i.Url).ToList()))
                .ForMember(dest => dest.PrimaryImageUrl, opt => opt.MapFrom(src => src.Images.FirstOrDefault(i => i.SortOrder == 0).Url))
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.GetAverageRating()))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews));

            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(x => x.Category.Name))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductSlug, opt => opt.MapFrom(src => src.Slug))
                .ForMember(dest => dest.MinPrice,
                    opt => opt.MapFrom(src =>
                        src.Variants
                           .Select(v => v.Price)
                           .DefaultIfEmpty(0)
                           .Min()))
                .ForMember(dest => dest.MaxPrice,
                    opt => opt.MapFrom(src =>
                        src.Variants
                           .Select(v => v.Price)
                           .DefaultIfEmpty(0)
                           .Max()))
                .ForMember(dest => dest.AverageRating, opt => opt.Ignore())
                .ForMember(dest => dest.ReviewsCount, opt => opt.Ignore());
            //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            //.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            //.ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))

            CreateMap<ShippingContact, ShippingContactResponse>()
                .ConstructUsing(src => new ShippingContactResponse(
                    src.Id,
                    src.FullName,
                    src.PhoneNumber,
                    src.Address.AddressLine1,
                    src.Address.AddressLine2,
                    src.Address.City,
                    src.Address.Region,
                    src.Address.PostalCode,
                    src.Address.CountryCode,
                    src.IsDefault
                ));

            CreateMap<FavoriteItem, FavoriteItemResponse>()
                .ForMember(dest => dest.FavoriteItemId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.VariantSlug, opt => opt.MapFrom(src => src.Product.Slug))
                .ForMember(dest => dest.ProductSlug, opt => opt.MapFrom(src => src.Product.Product.Slug))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Product.Brand))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.Product.Stock.Quantity))
                .ForMember(dest => dest.PrimaryImageUrl, opt => opt.MapFrom(src => src.Product.Images.Select(i => i.Url).FirstOrDefault()));

            CreateMap<ShippingContact, ShippingAddressSnapshot>()
                .ForMember(dest => dest.AddressLine1, opt => opt.MapFrom(src => src.Address.AddressLine1))
                .ForMember(dest => dest.AddressLine2, opt => opt.MapFrom(src => src.Address.AddressLine2))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Address.Region))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.Address.CountryCode));

            CreateMap<Application.Models.PriceRange, Domain.Models.PriceRange>();
            CreateMap<Application.Models.ProductSortBy, Domain.Catalog.Enums.ProductSortBy>();

            CreateMap<GetProductVariants.Query, ProductVariantsFilter>();

            CreateMap<Review, ReviewResponse>()
                .ForMember(dest => dest.ReviewId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => (int)src.Rating))
                .ForMember(dest => dest.UserName, opt => opt.Ignore());
        }
    }
}
