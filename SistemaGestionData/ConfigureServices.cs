using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaGestionData.Context;
using SistemaGestionData.DataAccess;
using SistemaGestionEntities;

namespace SistemaGestionData;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureDataLayer(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {
        //private Usuario userAdmin = new Usuario();

        services.AddDbContext<ProyectoFinalContext>((serviceProvider, options) =>
            {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                var connectionString = config.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            }
        );

        services.AddSingleton<IConfiguration>(configuration);

        services.AddScoped<ProductosDataAccess>();
        services.AddScoped<UsuariosDataAccess>();
        services.AddScoped<VentasDataAccess>();
        services.AddScoped<ProductosVendidosDataAccess>();
        services.AddScoped<LoginDataAccess>();

        return services;
    }
}