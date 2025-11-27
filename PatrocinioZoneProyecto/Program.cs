using Microsoft.EntityFrameworkCore;
using PatrocinioZoneProyecto.Data;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Controladores con vistas (MVC)
builder.Services.AddControllersWithViews();

// 2️⃣ Conexión a la base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3️⃣ Agregar SESSION (ANTES del Build)
builder.Services.AddSession();

var app = builder.Build();

// 4️⃣ Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // 5️⃣ Session SIEMPRE antes de MapControllerRoute

// Si tuvieras roles/autorización, va aquí:
// app.UseAuthorization();

// 6️⃣ Ruta por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}");

app.Run();

