using System;
using Microsoft.EntityFrameworkCore;
using SistemaGestionEntities;

namespace SistemaGestionData.Context;

public class ProyectoFinalContext : DbContext
{
    public DbSet<Producto> Productos { get; set; }

    public DbSet<Usuario> Usuarios { get; set; }

    public DbSet<Venta> Ventas { get; set; }

    public DbSet<ProductoVendido> ProductoVendido { get; set; }

    public ProyectoFinalContext()
        : base() { }

    public ProyectoFinalContext(DbContextOptions<ProyectoFinalContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //var connectionString = _configuration.GetConnectionString("ProyectoFinalCS");
            optionsBuilder.UseSqlServer(
                "Data Source=LAPTOP-HJU0A2KC;Initial Catalog=ProyectoFinalCS;Integrated Security=True;TrustServerCertificate=True"
                //"Server=LAPTOP-HJU0A2KC;Database=ProyectoFinalCS;Integrated Security=True;Trusted_Connection=True;"
            );
        }
    }

}
