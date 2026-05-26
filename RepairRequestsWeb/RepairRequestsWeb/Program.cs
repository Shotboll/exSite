using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RepairRequestsBusinessLogic.BusinessLogics;
using RepairRequestsContracts.BusinessLogicsContracts;
using RepairRequestsContracts.StoragesLogics;
using RepairRequestsDatabaseImplement.Database;
using RepairRequestsDatabaseImplement.Storages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<RepairRequestsDatabase>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IUserStorage, UserStorage>();
builder.Services.AddScoped<IDeviceTypeStorage, DeviceTypeStorage>();
builder.Services.AddScoped<IServiceStorage, ServiceStorage>();
builder.Services.AddScoped<IRepairRequestStorage, RepairRequestStorage>();

builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<IDeviceTypeLogic, DeviceTypeLogic>();
builder.Services.AddScoped<IServiceLogic, ServiceLogic>();
builder.Services.AddScoped<IRepairRequestLogic, RepairRequestLogic>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/Exception");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var database = scope.ServiceProvider.GetRequiredService<RepairRequestsDatabase>();
    database.Database.Migrate();
}

app.UseStatusCodePagesWithReExecute("/Error/StatusCode", "?code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
