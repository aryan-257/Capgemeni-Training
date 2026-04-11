using MediatR;
using FreshCart.Application.DTOs;
using FreshCart.Application.Commands.Cart;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace FreshCart.Application.Queries.Cart;

public class GetCartQuery : IRequest<CartDto?>
{
    public Guid CustomerId { get; set; }
    public Guid? StoreId { get; set; }
}

public class GetCartQueryHandler : IRequestHandler<GetCartQuery, CartDto?>
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCartQueryHandler> _logger;

    public GetCartQueryHandler(
        ICartRepository cartRepository,
        IMapper mapper,
        ILogger<GetCartQueryHandler> logger)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CartDto?> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var customerId = request.CustomerId;
        var storeId = request.StoreId;

        Domain.Entities.Cart? cart;
        
        if (storeId.HasValue)
        {
            cart = await _cartRepository.GetActiveCartAsync(customerId, storeId.Value);
        }
        else
        {
            // Get the most recent active cart
            cart = await _cartRepository.GetMostRecentActiveCartAsync(customerId);
        }

        if (cart == null)
        {
            return null;
        }

        var cartDto = _mapper.Map<CartDto>(cart);
        
        // Calculate totals
        cartDto.Subtotal = cart.CartItems.Sum(ci => ci.Quantity * ci.UnitPriceAtAdd);
        cartDto.TotalItems = cart.CartItems.Sum(ci => ci.Quantity);
        
        if (cartDto.BudgetTarget.HasValue)
        {
            cartDto.BudgetRemaining = cartDto.BudgetTarget.Value - cartDto.Subtotal;
        }

        _logger.LogInformation("Retrieved cart for customer {CustomerId}: {CartId}", 
            customerId, cart.CartId);

        return cartDto;
    }
}

// Extension to ICartRepository
public static class CartRepositoryExtensions
{
    public static async Task<Domain.Entities.Cart?> GetMostRecentActiveCartAsync(
        this ICartRepository repository, Guid customerId)
    {
        // This would be implemented in the concrete repository
        throw new NotImplementedException("To be implemented in Infrastructure layer");
    }
}