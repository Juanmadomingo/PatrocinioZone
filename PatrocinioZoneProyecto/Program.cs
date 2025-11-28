using Microsoft.EntityFrameworkCore;
using PatrocinioZoneProyecto.Data;

var builder = WebApplication.CreateBuilder(args);
// CONFIGURAR SQL SERVER
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// SESSION
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=ChooseType}/{id?}"
);

app.Run();
