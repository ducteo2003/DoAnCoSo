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
builder.Services.AddScoped<IChanDoanRepository, EFChandoanRepository>();
builder.Services.AddScoped<IDieutriRepository, EFDieutriRepository>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<Itinyte, EFTinYte>();
builder.Services.AddScoped<DatlichViewModel>();
builder.Services.AddSingleton<ZaloService>(provider => new ZaloService("5Xgb0XxD1nGM7weA4CuD714OhmCHrd5KHbc20W_BG4XpSvO3AzjGVYjPa1TSb6Dr2Jt61MwuOn4j6vW6Vw8_5pOwvbf0j4iC807c6Mgr9LiP9_rCOf8Y634HsLnPgtKoK1ZLG1FB2bLQHQX60zLL8bK4W7O7g5qOGmo-TmAW73vJDyaW1u0PJ6yBtmeKcpr5Rq_zEmpBEMnVITKHD_4815TKrmuurmXaSb6k3W7eJX1vMkjDVSyj8o1XqKuaqcXQ9K7B3th6474GPyWrP-G9K3j5g4upqKnOSMxXAGhtEsTmBfi21FPgHs11a1WKsM8ETWYq7JQEGLvbLuSj9-TNCo5r-bLIvmO-16_YB4BVJr90I8H0AEnQ5tKtCE2K9nBK01G"));

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
var app = builder.Build();

// Configure the HTTP request pipeline.s
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}



app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Datlich}/{id?}");
app.MapRazorPages();

app.Run();
