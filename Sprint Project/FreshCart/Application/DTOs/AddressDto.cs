namespace FreshCart.Application.DTOs;

public class AddressDto
{
    public Guid AddressId { get; set; }
    public Guid UserId { get; set; }
    public string Label { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool IsDefault { get; set; }
    public string? DeliveryInstructions { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateAddressDto
{
    public string Label { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public bool IsDefault { get; set; } = false;
    public string? DeliveryInstructions { get; set; }
}

public class UpdateAddressDto
{
    public string? Label { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public bool? IsDefault { get; set; }
    public string? DeliveryInstructions { get; set; }
}

public class DeliveryZoneCheckDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public class DeliveryZoneCheckResultDto
{
    public bool IsInZone { get; set; }
    public Guid? ZoneId { get; set; }
    public string? ZoneName { get; set; }
    public decimal? DeliveryFee { get; set; }
    public bool ExpressAvailable { get; set; }
    public decimal? ExpressFee { get; set; }
}