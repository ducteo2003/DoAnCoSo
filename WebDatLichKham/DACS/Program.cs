using DACS.Data;
using DACS.Repositories;
using DoAnCoSo.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Microsoft.Extensions.Options;
using System.Globalization;
using WebDatLichKham.Models;
using WebDatLichKham.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)); 

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddDefaultTokenProviders()
        .AddDefaultUI()
        .AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.LoginPath = $"Identity/Account/Login";
//    options.LoginPath = $"Identity/Account/Logout";
//    options.LoginPath = $"Identity/Account/AccessDenied";
//});
builder.Services.AddRazorPages();
builder.Services.AddScoped<IBenhNhanRepository, EFBenhNhanRepository>();
builder.Services.AddScoped<IBacsiRepository, EFBacsiRepository>();
builder.Services.AddScoped<ILichLamViecRepository, EFlichLamViecRepository>();
builder.Services.AddScoped<ILichKhamBenhRepository, EFLichkhamBenhRepository>();
builder.Services.AddScoped<IChuyenKhoaRepository, EFChuyenKhoaRepository>();
builder.Services.AddScoped<IChucVurepository,EFChucvuRepository>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<Itinyte, EFTinYte>();
builder.Services.AddScoped<DatlichViewModel>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(name: "Admin", pattern: "{area:exists}/{controller=Admin}/{acction=Index}/{id?}");
//    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Admin}/{action=Index}/{id?}");
//});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Datlich}/{id?}");
app.MapRazorPages();

app.Run();
