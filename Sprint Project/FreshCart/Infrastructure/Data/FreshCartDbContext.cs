using Microsoft.EntityFrameworkCore;
using FreshCart.Domain.Entities;
using System.Text.Json;

namespace FreshCart.Infrastructure.Data;

public class FreshCartDbContext : DbContext
{
    public FreshCartDbContext(DbContextOptions<FreshCartDbContext> options) : base(options)
    {
    }

    // DbSets
    public DbSet<User> Users { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OrderStatusHistory> OrderStatusHistory { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<DeliverySlot> DeliverySlots { get; set; }
    public DbSet<DeliveryZone> DeliveryZones { get; set; }
    public DbSet<DriverAssignment> DriverAssignments { get; set; }
    public DbSet<DriverLocation> DriverLocations { get; set; }
    public DbSet<RecurringList> RecurringLists { get; set; }
    public DbSet<RecurringListItem> RecurringListItems { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<PromotionRedemption> PromotionRedemptions { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Dispute> Disputes { get; set; }
    public DbSet<AdminAuditLog> AdminAuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure entity relationships and constraints
        ConfigureUserEntity(modelBuilder);
        ConfigureStoreEntity(modelBuilder);
        ConfigureProductEntity(modelBuilder);
        ConfigureCategoryEntity(modelBuilder);
        ConfigureCartEntity(modelBuilder);
        ConfigureOrderEntity(modelBuilder);
        ConfigureAddressEntity(modelBuilder);
        ConfigureDeliveryEntity(modelBuilder);
        ConfigureDriverEntity(modelBuilder);
        ConfigureRecurringListEntity(modelBuilder);
        ConfigurePromotionEntity(modelBuilder);
        ConfigureReviewEntity(modelBuilder);
        ConfigurePaymentEntity(modelBuilder);
        ConfigureDisputeEntity(modelBuilder);
        ConfigureAdminAuditLogEntity(modelBuilder);
    }
    private void ConfigureUserEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.RowVersion).IsRowVersion();
            entity.Property(e => e.Role).HasConversion<int>();
        });
    }

    private void ConfigureStoreEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId);
            entity.Property(e => e.RowVersion).IsRowVersion();
            entity.Property(e => e.DeliveryZoneIds)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<Guid>>(v, (JsonSerializerOptions?)null) ?? new List<Guid>());
        });
    }

    private void ConfigureProductEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId);
            entity.HasIndex(e => e.SKU);
            entity.HasIndex(e => e.UPC);
            entity.Property(e => e.RowVersion).IsRowVersion();
            entity.Property(e => e.Price).HasPrecision(10, 2);
            entity.Property(e => e.CompareAtPrice).HasPrecision(10, 2);
            entity.Property(e => e.AverageRating).HasPrecision(3, 2);
            
            entity.Property(e => e.DietaryTags)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>());
                    
            entity.Property(e => e.ImageUrls)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>());

            entity.HasOne(e => e.Store)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.StoreId);

            entity.HasOne(e => e.Category)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.CategoryId);
        });
    }

    private void ConfigureCategoryEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId);
            entity.HasIndex(e => e.Slug).IsUnique();
            entity.Property(e => e.RowVersion).IsRowVersion();

            entity.HasOne(e => e.ParentCategory)
                .WithMany(e => e.SubCategories)
                .HasForeignKey(e => e.ParentCategoryId);
        });
    }
    private void ConfigureCartEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId);
            entity.Property(e => e.RowVersion).IsRowVersion();
            entity.Property(e => e.Status).HasConversion<int>();
            entity.Property(e => e.BudgetTarget).HasPrecision(10, 2);

            entity.HasOne(e => e.Customer)
                .WithMany()
                .HasForeignKey(e => e.CustomerId);

            entity.HasOne(e => e.Store)
                .WithMany()
                .HasForeignKey(e => e.StoreId);
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.CartItemId);
            entity.Property(e => e.UnitPriceAtAdd).HasPrecision(10, 2);

            entity.HasOne(e => e.Cart)
                .WithMany(e => e.CartItems)
                .HasForeignKey(e => e.CartId);

            entity.HasOne(e => e.Product)
                .WithMany(e => e.CartItems)
                .HasForeignKey(e => e.ProductId);
        });
    }

    private void ConfigureOrderEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId);
            entity.Property(e => e.RowVersion).IsRowVersion();
            entity.Property(e => e.Status).HasConversion<int>();
            entity.Property(e => e.Subtotal).HasPrecision(10, 2);
            entity.Property(e => e.DeliveryFee).HasPrecision(10, 2);
            entity.Property(e => e.Discount).HasPrecision(10, 2);
            entity.Property(e => e.Tax).HasPrecision(10, 2);
            entity.Property(e => e.Total).HasPrecision(10, 2);

            entity.HasOne(e => e.Customer)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.CustomerId);

            entity.HasOne(e => e.Store)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.StoreId);

            entity.HasOne(e => e.AssignedDriver)
                .WithMany()
                .HasForeignKey(e => e.AssignedDriverId);

            entity.HasOne(e => e.DeliveryAddress)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.DeliveryAddressId);

            entity.HasOne(e => e.Slot)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.SlotId);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId);
            entity.Property(e => e.UnitPrice).HasPrecision(10, 2);
            entity.Property(e => e.LineTotal).HasPrecision(10, 2);

            entity.HasOne(e => e.Order)
                .WithMany(e => e.OrderItems)
                .HasForeignKey(e => e.OrderId);

            entity.HasOne(e => e.Product)
                .WithMany(e => e.OrderItems)
                .HasForeignKey(e => e.ProductId);

            entity.HasOne(e => e.OriginalProduct)
                .WithMany()
                .HasForeignKey(e => e.OriginalProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<OrderStatusHistory>(entity =>
        {
            entity.HasKey(e => e.StatusHistoryId);
            entity.Property(e => e.Status).HasConversion<int>();

            entity.HasOne(e => e.Order)
                .WithMany(e => e.StatusHistory)
                .HasForeignKey(e => e.OrderId);
        });
    }
    private void ConfigureAddressEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId);
            entity.Property(e => e.RowVersion).IsRowVersion();

            entity.HasOne(e => e.User)
                .WithMany(e => e.Addresses)
                .HasForeignKey(e => e.UserId);
        });
    }

    private void ConfigureDeliveryEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeliverySlot>(entity =>
        {
            entity.HasKey(e => e.SlotId);
            entity.Property(e => e.RowVersion).IsRowVersion();
            entity.Property(e => e.DeliveryFee).HasPrecision(10, 2);

            entity.HasOne(e => e.Store)
                .WithMany(e => e.DeliverySlots)
                .HasForeignKey(e => e.StoreId);

            entity.HasOne(e => e.Zone)
                .WithMany(e => e.DeliverySlots)
                .HasForeignKey(e => e.ZoneId);
        });

        modelBuilder.Entity<DeliveryZone>(entity =>
        {
            entity.HasKey(e => e.ZoneId);
            entity.Property(e => e.RowVersion).IsRowVersion();
            entity.Property(e => e.BaseDeliveryFee).HasPrecision(10, 2);
            entity.Property(e => e.ExpressFeeAdd).HasPrecision(10, 2);
        });
    }

    private void ConfigureDriverEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DriverAssignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId);
            entity.Property(e => e.RowVersion).IsRowVersion();
            entity.Property(e => e.Status).HasConversion<int>();

            entity.HasOne(e => e.Order)
                .WithOne(e => e.DriverAssignment)
                .HasForeignKey<DriverAssignment>(e => e.OrderId);

            entity.HasOne(e => e.Driver)
                .WithMany(e => e.DriverAssignments)
                .HasForeignKey(e => e.DriverId);
        });

        modelBuilder.Entity<DriverLocation>(entity =>
        {
            entity.HasKey(e => e.LocationId);

            entity.HasOne(e => e.Driver)
                .WithMany()
                .HasForeignKey(e => e.DriverId);

            entity.HasOne(e => e.Order)
                .WithMany()
                .HasForeignKey(e => e.OrderId);
        });
    }
    private void ConfigureRecurringListEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecurringList>(entity =>
        {
            entity.HasKey(e => e.RecurringListId);
            entity.Property(e => e.RowVersion).IsRowVersion();
            entity.Property(e => e.Schedule).HasConversion<int>();
            entity.Property(e => e.BudgetCap).HasPrecision(10, 2);

            entity.HasOne(e => e.Customer)
                .WithMany(e => e.RecurringLists)
                .HasForeignKey(e => e.CustomerId);
        });

        modelBuilder.Entity<RecurringListItem>(entity =>
        {
            entity.HasKey(e => e.RecurringListItemId);

            entity.HasOne(e => e.RecurringList)
                .WithMany(e => e.RecurringListItems)
                .HasForeignKey(e => e.RecurringListId);

            entity.HasOne(e => e.Product)
                .WithMany()
                .HasForeignKey(e => e.ProductId);
        });
    }

    private void ConfigurePromotionEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId);
            entity.Property(e => e.RowVersion).IsRowVersion();
            entity.Property(e => e.Type).HasConversion<int>();
            entity.Property(e => e.AppliesTo).HasConversion<int>();
            entity.Property(e => e.Value).HasPrecision(10, 2);
            entity.Property(e => e.MinOrderValue).HasPrecision(10, 2);

            entity.HasOne(e => e.Store)
                .WithMany(e => e.Promotions)
                .HasForeignKey(e => e.StoreId);
        });

        modelBuilder.Entity<PromotionRedemption>(entity =>
        {
            entity.HasKey(e => e.RedemptionId);
            entity.Property(e => e.DiscountAmount).HasPrecision(10, 2);

            entity.HasOne(e => e.Promotion)
                .WithMany(e => e.PromotionRedemptions)
                .HasForeignKey(e => e.PromotionId);

            entity.HasOne(e => e.Order)
                .WithMany()
                .HasForeignKey(e => e.OrderId);

            entity.HasOne(e => e.Customer)
                .WithMany()
                .HasForeignKey(e => e.CustomerId);
        });
    }
    private void ConfigureReviewEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId);
            entity.Property(e => e.RowVersion).IsRowVersion();
            entity.Property(e => e.ReviewType).HasConversion<int>();
            
            entity.Property(e => e.PhotoUrls)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>());

            entity.HasOne(e => e.Order)
                .WithMany()
                .HasForeignKey(e => e.OrderId);

            entity.HasOne(e => e.Customer)
                .WithMany(e => e.Reviews)
                .HasForeignKey(e => e.CustomerId);
        });
    }

    private void ConfigurePaymentEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId);
            entity.Property(e => e.RowVersion).IsRowVersion();
            entity.Property(e => e.Status).HasConversion<int>();
            entity.Property(e => e.Amount).HasPrecision(10, 2);
            entity.Property(e => e.RefundedAmount).HasPrecision(10, 2);

            entity.HasOne(e => e.Order)
                .WithMany()
                .HasForeignKey(e => e.OrderId);
        });
    }

    private void ConfigureDisputeEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dispute>(entity =>
        {
            entity.HasKey(e => e.DisputeId);
            entity.Property(e => e.RowVersion).IsRowVersion();
            entity.Property(e => e.Type).HasConversion<int>();
            entity.Property(e => e.Status).HasConversion<int>();
            entity.Property(e => e.ResolutionType).HasConversion<int>();
            entity.Property(e => e.RefundAmount).HasPrecision(10, 2);

            entity.HasOne(e => e.Order)
                .WithMany()
                .HasForeignKey(e => e.OrderId);

            entity.HasOne(e => e.Customer)
                .WithMany()
                .HasForeignKey(e => e.CustomerId);

            entity.HasOne(e => e.ResolvedByAdmin)
                .WithMany()
                .HasForeignKey(e => e.ResolvedByAdminId);
        });
    }

    private void ConfigureAdminAuditLogEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminAuditLog>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.HasOne(e => e.Admin)
                .WithMany()
                .HasForeignKey(e => e.AdminId);
        });
    }
}