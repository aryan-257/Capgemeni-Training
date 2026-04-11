namespace FreshCart.Application.DTOs;

public class UserDto
{
    public Guid UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Mobile { get; set; }
    public string Role { get; set; } = string.Empty;
    public bool IsVerified { get; set; }
    public bool IsBanned { get; set; }
    public bool IsAvailable { get; set; }
    public int LoyaltyPoints { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class RegisterUserDto
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string? Mobile { get; set; }
    public string Role { get; set; } = "Customer";
    public List<GuestCartItemDto>? GuestCart { get; set; }
}

public class LoginDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? TwoFactorCode { get; set; }
}

public class LoginResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public UserDto User { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
}

public class GuestCartItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public string? SpecialInstructions { get; set; }
}