using APIRESTMecanico.datos.Datos;
using APIRESTMecanico.datos.Interface;
using APIRESTMecanico.datos.Modelo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IGenericRepository<Categoria>, CategoriaDAO>();
builder.Services.AddScoped<IGenericRepository<Producto>, ProductoDAO>();
builder.Services.AddScoped<IGenericRepository<Cliente>, ClienteDAO>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
