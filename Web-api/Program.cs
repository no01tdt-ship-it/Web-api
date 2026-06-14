using Microsoft.EntityFrameworkCore;
using Web_api.Data;
using Web_api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register controllers with views support (before Build)
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDb>(options =>
    options.UseSqlServer(connectionString));

// GỌI HÀM MỞ RỘNG ĐỂ KÍCH HOẠT DI (Gọn gàng trong 1 dòng!)
builder.Services.AddInfrastructureServices();

var app = builder.Build();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseStaticFiles();
app.MapControllers();
app.Run();

