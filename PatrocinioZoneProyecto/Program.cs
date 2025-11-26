using Microsoft.EntityFrameworkCore;
using PatrocinioZoneProyecto.Data;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Agregar controladores con vistas (NO solo controllers)
builder.Services.AddControllersWithViews();

// 2️⃣ Configurar la conexión a la base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 3️⃣ Configurar pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// 4️⃣ Habilitar rutas MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Club}/{action=Index}/{id?}");

app.Run();
