using SevicesDemo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Add our calculator services
builder.Services.AddScoped<IBasicCalculatorService, BasicCalculatorService>();
builder.Services.AddScoped<IAdvancedCalculatorService, AdvancedCalculatorService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Calculator}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
