using MediatR;
using FreshCart.Application.DTOs;
using FreshCart.Domain.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace FreshCart.Application.Commands.Cart;

public class AddToCartCommand : IRequest<CartDto>
{
    public Guid CustomerId { get; set; }
    public AddToCartDto AddToCartDto { get; set; } = null!;
}

public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, CartDto>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly ISmartCartService _smartCartService;
    private readonly IMapper _mapper;
    private readonly ILogger<AddToCartCommandHandler> _logger;

    public AddToCartCommandHandler(
        ICartRepository cartRepository,
        IProductRepository productRepository,
        ISmartCartService smartCartService,
        IMapper mapper,
        ILogger<AddToCartCommandHandler> logger)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _smartCartService = smartCartService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CartDto> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        var customerId = request.CustomerId;
        var addToCartDto = request.AddToCartDto;

        // Get product details
        var product = await _productRepository.GetByIdAsync(addToCartDto.ProductId);
        if (product == null)
        {
            throw new ArgumentException("Product not found.");
        }

        // Check stock availability
        if (product.StockQty < addToCartDto.Quantity)
        {
            throw new InvalidOperationException($"Insufficient stock. Available: {product.StockQty}");
        }

        // Get or create active cart for customer and store
        var cart = await _cartRepository.GetActiveCartAsync(customerId, product.StoreId);
        if (cart == null)
        {
            cart = new Domain.Entities.Cart
            {
                CartId = Guid.NewGuid(),
                CustomerId = customerId,
                StoreId = product.StoreId,
                Status = CartStatus.Active,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await _cartRepository.CreateAsync(cart);
        }

        // Check if product already exists in cart
        var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == addToCartDto.ProductId);
        if (existingItem != null)
        {
            // Update quantity
            existingItem.Quantity += addToCartDto.Quantity;
            existingItem.UpdatedAt = DateTime.UtcNow;
            if (!string.IsNullOrEmpty(addToCartDto.SpecialInstructions))
            {
                existingItem.SpecialInstructions = addToCartDto.SpecialInstructions;
            }
        }
        else
        {
            // Add new item
            var cartItem = new CartItem
            {
                CartItemId = Guid.NewGuid(),
                CartId = cart.CartId,
                ProductId = addToCartDto.ProductId,
                Quantity = addToCartDto.Quantity,
                UnitPriceAtAdd = product.Price,
                SpecialInstructions = addToCartDto.SpecialInstructions,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            cart.CartItems.Add(cartItem);
        }

        cart.UpdatedAt = DateTime.UtcNow;
        await _cartRepository.UpdateAsync(cart);

        // Trigger Smart Cart suggestions
        await _smartCartService.UpdateSuggestionsAsync(cart.CartId);

        _logger.LogInformation("Product added to cart: {ProductId} for customer {CustomerId}", 
            addToCartDto.ProductId, customerId);

        return _mapper.Map<CartDto>(cart);
    }
}

public interface ICartRepository
{
    Task<Domain.Entities.Cart?> GetActiveCartAsync(Guid customerId, Guid storeId);
    Task<Domain.Entities.Cart?> GetByIdAsync(Guid cartId);
    Task<Domain.Entities.Cart> CreateAsync(Domain.Entities.Cart cart);
    Task<Domain.Entities.Cart> UpdateAsync(Domain.Entities.Cart cart);
    Task DeleteAsync(Guid cartId);
}

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid productId);
    Task<IEnumerable<Product>> SearchAsync(ProductSearchDto searchDto);
    Task<Product> CreateAsync(Product product);
    Task<Product> UpdateAsync(Product product);
}

public interface ISmartCartService
{
    Task UpdateSuggestionsAsync(Guid cartId);
    Task<IEnumerable<ProductDto>> GetSuggestionsAsync(Guid cartId);
    Task<IEnumerable<ProductDto>> GetSubstitutionsAsync(Guid productId);
}