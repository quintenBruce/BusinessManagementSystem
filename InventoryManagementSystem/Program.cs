using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

builder.Services.AddDbContext<DbContext, OrdersContext>(option => option.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=inventory_management;Integrated Security=True;"));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<OrdersContext>().AddDefaultTokenProviders();



builder.Services.AddScoped<DbContext, OrdersContext>();
builder.Services.AddScoped<ICalender, CalenderService>();
builder.Services.AddTransient<ISendGrid, SendGridService>();
builder.Services.AddHttpClient<WebApiService>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 10;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    //options.SignIn.RequireConfirmedAccount = true;
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();