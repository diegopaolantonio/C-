using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SistemaGestionData.Context;
using System.IO;

public class ProyectoFinalContextFactory : IDesignTimeDbContextFactory<ProyectoFinalContext>
{
    public ProyectoFinalContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../SistemaGestionWebApi");
        Console.WriteLine($"Base Path: {basePath}");

        // Construir la configuración desde el archivo appsettings.Development.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        // Obtener la cadena de conexión
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Imprimir la cadena de conexión para depuración
        Console.WriteLine($"Connection String: {connectionString}");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("La cadena de conexión es nula o vacía. Verifica los archivos de configuración.");
        }

        // Configurar las opciones de DbContext
        var optionsBuilder = new DbContextOptionsBuilder<ProyectoFinalContext>();
        optionsBuilder.UseSqlServer(connectionString);

        // Crear y devolver el contexto
        return new ProyectoFinalContext(optionsBuilder.Options, configuration);
    }
}
