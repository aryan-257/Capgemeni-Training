using AutoMapper;
using FreshCart.Domain.Entities;
using FreshCart.Application.DTOs;

namespace FreshCart.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User mappings
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
        
        CreateMap<RegisterUserDto, User>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.Parse<UserRole>(src.Role)))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Product mappings
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.Name))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        
        CreateMap<CreateProductDto, Product>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Cart mappings
        CreateMap<Cart, CartDto>()
            .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.Name))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.CartItems));
        
        CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.Product.Brand))
            .ForMember(dest => dest.ProductImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrls.FirstOrDefault() ?? ""))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Product.Unit))
            .ForMember(dest => dest.LineTotal, opt => opt.MapFrom(src => src.Quantity * src.UnitPriceAtAdd))
            .ForMember(dest => dest.IsOutOfStock, opt => opt.MapFrom(src => src.Product.StockQty == 0));

        // Order mappings
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.Name))
            .ForMember(dest => dest.DriverName, opt => opt.MapFrom(src => src.AssignedDriver != null ? src.AssignedDriver.FullName : null))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));
        
        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.Product.Brand))
            .ForMember(dest => dest.ProductImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrls.FirstOrDefault() ?? ""))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Product.Unit))
            .ForMember(dest => dest.OriginalProductName, opt => opt.MapFrom(src => src.OriginalProduct != null ? src.OriginalProduct.Name : null));

        // Address mappings
        CreateMap<Address, AddressDto>();
        CreateMap<CreateAddressDto, Address>()
            .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // DeliverySlot mappings
        CreateMap<DeliverySlot, DeliverySlotDto>()
            .ForMember(dest => dest.AvailableSlots, opt => opt.MapFrom(src => src.MaxCapacity - src.CurrentBookings))
            .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.CurrentBookings < src.MaxCapacity))
            .ForMember(dest => dest.DisplayText, opt => opt.MapFrom(src => FormatSlotDisplayText(src)));

        // Store mappings
        CreateMap<Store, StoreDto>();

        // Category mappings
        CreateMap<Category, CategoryDto>();

        // Review mappings
        CreateMap<Review, Controllers.ReviewDto>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FullName));

        // Promotion mappings
        CreateMap<Promotion, PromotionDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.AppliesTo, opt => opt.MapFrom(src => src.AppliesTo.ToString()));
    }

    private static string FormatSlotDisplayText(DeliverySlot slot)
    {
        var dateText = slot.Date == DateOnly.FromDateTime(DateTime.Today) ? "Today" :
                      slot.Date == DateOnly.FromDateTime(DateTime.Today.AddDays(1)) ? "Tomorrow" :
                      slot.Date.ToString("MMM dd");
        
        var startTime = slot.StartTime.ToString(@"h\:mm");
        var endTime = slot.EndTime.ToString(@"h\:mm");
        var period = slot.StartTime.Hours < 12 ? "AM" : "PM";
        
        return $"{dateText} {startTime} - {endTime} {period}";
    }
}

// Additional DTOs referenced in mappings
public class StoreDto
{
    public Guid StoreId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Address { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? LogoUrl { get; set; }
    public decimal AverageRating { get; set; }
    public bool IsActive { get; set; }
}

public class CategoryDto
{
    public Guid CategoryId { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int Level { get; set; }
    public string? IconUrl { get; set; }
    public int SortOrder { get; set; }
    public List<CategoryDto> SubCategories { get; set; } = new();
}

public class PromotionDto
{
    public Guid PromotionId { get; set; }
    public Guid? StoreId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string AppliesTo { get; set; } = string.Empty;
    public decimal MinOrderValue { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public int? UsageLimit { get; set; }
    public int UsageCount { get; set; }
    public bool IsActive { get; set; }
    public string? CouponCode { get; set; }
}