namespace FreshCart.Application.DTOs;

public class DeliverySlotDto
{
    public Guid SlotId { get; set; }
    public Guid StoreId { get; set; }
    public Guid ZoneId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int MaxCapacity { get; set; }
    public int CurrentBookings { get; set; }
    public int AvailableSlots { get; set; }
    public bool IsExpressSlot { get; set; }
    public decimal DeliveryFee { get; set; }
    public DateOnly Date { get; set; }
    public string DisplayText { get; set; } = string.Empty; // e.g., "Today 2:00 PM - 4:00 PM"
    public bool IsAvailable { get; set; }
}

public class DeliverySlotSearchDto
{
    public Guid StoreId { get; set; }
    public Guid? ZoneId { get; set; }
    public DateOnly? Date { get; set; }
    public bool IncludeExpress { get; set; } = true;
    public int DaysAhead { get; set; } = 7;
}

public class CreateDeliverySlotDto
{
    public Guid StoreId { get; set; }
    public Guid ZoneId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int MaxCapacity { get; set; }
    public bool IsExpressSlot { get; set; } = false;
    public decimal DeliveryFee { get; set; }
    public DateOnly Date { get; set; }
}

public class UpdateDeliverySlotDto
{
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public int? MaxCapacity { get; set; }
    public decimal? DeliveryFee { get; set; }
    public bool? IsExpressSlot { get; set; }
}