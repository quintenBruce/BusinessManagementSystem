using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

builder.Services.AddDbContext<DbContext, OrdersContext>(option => option.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=inventory_management;Integrated Security=True;"));
builder.Services.AddScoped<DbContext, OrdersContext>();
builder.Services.AddScoped<IProduct, ProductService>();
builder.Services.AddScoped<ICustomer, CustomerService>();
builder.Services.AddScoped<IOrder, OrderService>();
builder.Services.AddScoped<ICategory, CategoryService>();
builder.Services.AddScoped<IOrderGroup, OrderGroupService>();
builder.Services.AddScoped<IPaymentHistory, PaymentHistoryService>();
builder.Services.AddScoped<ICalender, CalenderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
