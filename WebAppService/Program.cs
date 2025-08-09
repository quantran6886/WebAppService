using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NotificationLibrary.NotificationControl;
using WebAppService.Middlewares;
using WebAppService.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký DbContext
builder.Services.AddDbContext<XEntitiesContex>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Cấu hình Identity XEntitiesContex
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<XEntitiesContex>()
    .AddDefaultTokenProviders();

// Đăng ký SignalR
builder.Services.AddSignalR();
builder.Services.AddScoped<INotificationService, NotificationService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian sống của session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSingleton<DapperConnection>();

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = long.MaxValue;
    options.MultipartHeadersLengthLimit = int.MaxValue;
});

builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Admin/Login/Login";
    options.AccessDeniedPath = "/Admin/Login/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/notfound/notfound");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
//app.UseMiddleware<InvalidDomainMiddleware>();
// Thêm SignalR Hub
app.MapHub<UserStatusHub>("/userStatusHub");
app.MapHub<NotificationHub>("/notificationHub");

//admin
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=HomePage1}/{id?}");
    endpoints.MapDefaultControllerRoute();
});

//frontend

app.MapControllerRoute(
    name: "DanhSachBacSi",
    pattern: "danh-sach-bac-si",
    defaults: new { controller = "Doctor", action = "ListDoctor" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=GioiThieu}/{action=GioiThieu}/{id?}");

app.Run();
