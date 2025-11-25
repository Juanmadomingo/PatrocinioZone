using Microsoft.EntityFrameworkCore;
using PatrocinioZoneProyecto.Data;

var builder = WebApplication.CreateBuilder(args);

// ====================================
// 🔌 1. Cadena de conexión SQL Server
// ====================================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=localhost;Database=PatrocinioDB;User Id=TU_USUARIO_SQL;Password=TU_CONTRASEÑA_SQL;TrustServerCertificate=True;";

// ====================================
// 🧱 2. Registrar DbContext
// ====================================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// ====================================
// 🌐 3. Agregar controladores y vistas MVC
// ====================================
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ====================================
// 🏁 4. Middlewares
// ====================================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// ====================================
// 🏠 5. Rutas por defecto
// ====================================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");
app.Run();

