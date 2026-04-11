using FluentValidation;
using FreshCart.Application.DTOs;

namespace FreshCart.Application.Validators;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required")
            .Length(2, 100).WithMessage("Full name must be between 2 and 100 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(255).WithMessage("Email must not exceed 255 characters");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]")
            .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Password confirmation is required")
            .Equal(x => x.Password).WithMessage("Passwords do not match");

        RuleFor(x => x.Mobile)
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid mobile number format")
            .When(x => !string.IsNullOrEmpty(x.Mobile));

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required")
            .Must(BeValidRole).WithMessage("Invalid role specified");
    }

    private static bool BeValidRole(string role)
    {
        return role is "Customer" or "StoreManager" or "Driver" or "Admin";
    }
}

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");

        RuleFor(x => x.TwoFactorCode)
            .Length(6).WithMessage("Two-factor code must be 6 digits")
            .Matches(@"^\d{6}$").WithMessage("Two-factor code must contain only digits")
            .When(x => !string.IsNullOrEmpty(x.TwoFactorCode));
    }
}

public class AddToCartDtoValidator : AbstractValidator<AddToCartDto>
{
    public AddToCartDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Product ID is required");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0")
            .LessThanOrEqualTo(100).WithMessage("Quantity cannot exceed 100");

        RuleFor(x => x.SpecialInstructions)
            .MaximumLength(500).WithMessage("Special instructions cannot exceed 500 characters")
            .When(x => !string.IsNullOrEmpty(x.SpecialInstructions));
    }
}

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(x => x.StoreId)
            .NotEmpty().WithMessage("Store ID is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required")
            .Length(2, 200).WithMessage("Product name must be between 2 and 200 characters");

        RuleFor(x => x.Brand)
            .MaximumLength(100).WithMessage("Brand name cannot exceed 100 characters")
            .When(x => !string.IsNullOrEmpty(x.Brand));

        RuleFor(x => x.SKU)
            .MaximumLength(50).WithMessage("SKU cannot exceed 50 characters")
            .When(x => !string.IsNullOrEmpty(x.SKU));

        RuleFor(x => x.UPC)
            .MaximumLength(20).WithMessage("UPC cannot exceed 20 characters")
            .Matches(@"^\d+$").WithMessage("UPC must contain only digits")
            .When(x => !string.IsNullOrEmpty(x.UPC));

        RuleFor(x => x.Description)
            .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category ID is required");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0")
            .LessThan(10000).WithMessage("Price cannot exceed $10,000");

        RuleFor(x => x.CompareAtPrice)
            .GreaterThan(x => x.Price).WithMessage("Compare at price must be greater than the regular price")
            .When(x => x.CompareAtPrice.HasValue);

        RuleFor(x => x.Unit)
            .NotEmpty().WithMessage("Unit is required")
            .MaximumLength(20).WithMessage("Unit cannot exceed 20 characters");

        RuleFor(x => x.StockQty)
            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative");

        RuleFor(x => x.LowStockThreshold)
            .GreaterThanOrEqualTo(0).WithMessage("Low stock threshold cannot be negative")
            .LessThanOrEqualTo(1000).WithMessage("Low stock threshold cannot exceed 1000");

        RuleFor(x => x.ImageUrls)
            .Must(HaveValidImageUrls).WithMessage("Invalid image URL format")
            .Must(HaveMaxFiveImages).WithMessage("Maximum 5 images allowed")
            .When(x => x.ImageUrls != null && x.ImageUrls.Any());
    }

    private static bool HaveValidImageUrls(List<string> imageUrls)
    {
        return imageUrls.All(url => Uri.TryCreate(url, UriKind.Absolute, out _));
    }

    private static bool HaveMaxFiveImages(List<string> imageUrls)
    {
        return imageUrls.Count <= 5;
    }
}

public class CreateAddressDtoValidator : AbstractValidator<CreateAddressDto>
{
    public CreateAddressDtoValidator()
    {
        RuleFor(x => x.Label)
            .NotEmpty().WithMessage("Address label is required")
            .Length(1, 100).WithMessage("Label must be between 1 and 100 characters");

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Street address is required")
            .Length(5, 200).WithMessage("Street address must be between 5 and 200 characters");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required")
            .Length(2, 100).WithMessage("City must be between 2 and 100 characters");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("State is required")
            .Length(2, 100).WithMessage("State must be between 2 and 100 characters");

        RuleFor(x => x.PostalCode)
            .NotEmpty().WithMessage("Postal code is required")
            .Length(3, 20).WithMessage("Postal code must be between 3 and 20 characters");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required")
            .Length(2, 100).WithMessage("Country must be between 2 and 100 characters");

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90")
            .When(x => x.Latitude.HasValue);

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180")
            .When(x => x.Longitude.HasValue);

        RuleFor(x => x.DeliveryInstructions)
            .MaximumLength(500).WithMessage("Delivery instructions cannot exceed 500 characters")
            .When(x => !string.IsNullOrEmpty(x.DeliveryInstructions));
    }
}