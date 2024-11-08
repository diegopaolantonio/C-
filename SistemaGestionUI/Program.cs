using SistemaGestionUI.ClientServices;
using SistemaGestionUI.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddTransient<ProductosService>();
builder.Services.AddTransient<UsuariosService>();
builder.Services.AddTransient<VentasService>();
builder.Services.AddTransient<ProductosVendidosService>();

builder.Services.AddHttpClient<ProductosService>(
    client => client.BaseAddress = new Uri($"http://localhost:5252/api/productos/")
    );

builder.Services.AddHttpClient<UsuariosService>(
    client => client.BaseAddress = new Uri($"{builder.Configuration["ApiUrl"]}/api/usuarios/")
    );

builder.Services.AddHttpClient<VentasService>(
    client => client.BaseAddress = new Uri($"{builder.Configuration["ApiUrl"]}/api/ventas/")
    );

builder.Services.AddHttpClient<ProductosVendidosService>(
    client => client.BaseAddress = new Uri($"{builder.Configuration["ApiUrl"]}/api/productosvendidos/")
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
