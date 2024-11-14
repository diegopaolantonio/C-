using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaGestionData.Context;
using SistemaGestionData.DataAccess;

namespace SistemaGestionData;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureDataLayer(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {
        services.AddDbContext<ProyectoFinalContext>(
            optionBuilder =>
            {
                //var connectionString = configuration.GetConnectionString("ProyectoFinalCS");
                optionBuilder.UseSqlServer("Data Source=LAPTOP-HJU0A2KC;Initial Catalog=ProyectoFinalCS;Integrated Security=True;TrustServerCertificate=True");
            }
        );
        services.AddScoped<ProductosDataAccess>();
        services.AddScoped<UsuariosDataAccess>();
        services.AddScoped<VentasDataAccess>();
        services.AddScoped<ProductosVendidosDataAccess>();
        services.AddScoped<LoginDataAccess>();
        return services;
    }
}