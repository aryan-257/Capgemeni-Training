using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Security.Claims;
using FreshCart.Application.Commands.Cart;
using FreshCart.Application.Queries.Cart;
using FreshCart.Application.DTOs;

namespace FreshCart.Controllers;

[ApiController]
[Route("api/v1/cart")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CartController> _logger;

    public CartController(IMediator mediator, ILogger<CartController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    private Guid GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.Parse(userIdClaim ?? throw new UnauthorizedAccessException("User ID not found in token"));
    }

    /// <summary>
    /// Get current user's cart
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<CartDto>> GetCart([FromQuery] Guid? storeId = null)
    {
        try
        {
            var customerId = GetCurrentUserId();
            var query = new GetCartQuery { CustomerId = customerId, StoreId = storeId };
            var result = await _mediator.Send(query);
            
            if (result == null)
            {
                return Ok(new CartDto { CustomerId = customerId });
            }
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting cart");
            return StatusCode(500, new { message = "An error occurred while retrieving cart" });
        }
    }

    /// <summary>
    /// Add product to cart
    /// </summary>
    [HttpPost("items")]
    public async Task<ActionResult<CartDto>> AddToCart([FromBody] AddToCartDto addToCartDto)
    {
        try
        {
            var customerId = GetCurrentUserId();
            var command = new AddToCartCommand { CustomerId = customerId, AddToCartDto = addToCartDto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding to cart");
            return StatusCode(500, new { message = "An error occurred while adding to cart" });
        }
    }

    /// <summary>
    /// Update cart item quantity
    /// </summary>
    [HttpPatch("items/{itemId}")]
    public async Task<ActionResult<CartDto>> UpdateCartItem(Guid itemId, [FromBody] UpdateCartItemDto updateDto)
    {
        try
        {
            var customerId = GetCurrentUserId();
            // TODO: Implement UpdateCartItemCommand
            return Ok(new CartDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating cart item {ItemId}", itemId);
            return StatusCode(500, new { message = "An error occurred while updating cart item" });
        }
    }

    /// <summary>
    /// Remove item from cart
    /// </summary>
    [HttpDelete("items/{itemId}")]
    public async Task<ActionResult<CartDto>> RemoveFromCart(Guid itemId)
    {
        try
        {
            var customerId = GetCurrentUserId();
            // TODO: Implement RemoveFromCartCommand
            return Ok(new CartDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing from cart");
            return StatusCode(500, new { message = "An error occurred while removing from cart" });
        }
    }

    /// <summary>
    /// Clear entire cart
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> ClearCart([FromQuery] Guid? storeId = null)
    {
        try
        {
            var customerId = GetCurrentUserId();
            // TODO: Implement ClearCartCommand
            return Ok(new { message = "Cart cleared successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error clearing cart");
            return StatusCode(500, new { message = "An error occurred while clearing cart" });
        }
    }

    /// <summary>
    /// Set or update cart budget target
    /// </summary>
    [HttpPatch("budget")]
    public async Task<ActionResult<CartDto>> SetBudget([FromBody] SetBudgetDto setBudgetDto)
    {
        try
        {
            var customerId = GetCurrentUserId();
            // TODO: Implement SetBudgetCommand
            return Ok(new CartDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting budget");
            return StatusCode(500, new { message = "An error occurred while setting budget" });
        }
    }

    /// <summary>
    /// Apply product substitution
    /// </summary>
    [HttpPost("substitute")]
    public async Task<ActionResult<CartDto>> SubstituteProduct([FromBody] SubstituteProductDto substituteDto)
    {
        try
        {
            var customerId = GetCurrentUserId();
            // TODO: Implement SubstituteProductCommand
            return Ok(new CartDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error substituting product");
            return StatusCode(500, new { message = "An error occurred while substituting product" });
        }
    }

    /// <summary>
    /// Apply bundle promotion to cart
    /// </summary>
    [HttpPost("apply-bundle")]
    public async Task<ActionResult<CartDto>> ApplyBundle([FromBody] ApplyBundleDto applyBundleDto)
    {
        try
        {
            var customerId = GetCurrentUserId();
            // TODO: Implement ApplyBundleCommand
            return Ok(new CartDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error applying bundle");
            return StatusCode(500, new { message = "An error occurred while applying bundle" });
        }
    }

    /// <summary>
    /// Add item to cart via voice transcript
    /// </summary>
    [HttpPost("voice-add")]
    public async Task<ActionResult<CartDto>> VoiceAdd([FromBody] VoiceAddDto voiceAddDto)
    {
        try
        {
            var customerId = GetCurrentUserId();
            // TODO: Implement VoiceAddCommand
            return Ok(new CartDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing voice add");
            return StatusCode(500, new { message = "An error occurred while processing voice input" });
        }
    }
}