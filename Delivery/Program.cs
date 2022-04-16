using Delivery;
using Delivery.AutoMapper;
using Delivery.Hubs;
using Delivery.Infrastructure.Data;
using Delivery.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Delivery.Infrastructure.SeedDataBase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DeliveryDbContext>(options =>
{
    options.UseSqlServer(connectionString).UseLazyLoadingProxies();
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<DeliveryUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+абвгдежзийклмнопрстуфхчцшщьъюяАБВГДЕЖЗИЙКЛМНОПРСТУФХЧЦШЩЬЪЮЯ ";
            options.User.RequireUniqueEmail = true;
        })
    .AddRoles<DeliveryRole>()
    .AddEntityFrameworkStores<DeliveryDbContext>();
builder.Services.AddAutoMapper(profile => { profile.AddProfile(typeof(AutoMapperConfiguration)); });
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator",
        authBuilder =>
        {
            authBuilder.RequireRole("Administrator");
        });
});
builder.Services.AddSignalR(
              options =>
              {
                  options.EnableDetailedErrors = true;
              });
builder.Services.AddStartupServices(builder.Configuration);
var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<DeliveryDbContext>();

    if (app.Environment.IsDevelopment()) dbContext.Database.Migrate();

    await new DeliveryDbSeeder().SeedAsync(dbContext, app.Services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    
}
else
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

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<OrderHub>("/orderHub");
    endpoints.MapHub<UserOrdersHub>("/userOrdersHub");
    endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.MapRazorPages();
app.Run();
