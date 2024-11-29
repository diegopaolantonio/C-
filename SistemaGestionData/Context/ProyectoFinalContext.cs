using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SistemaGestionEntities;

namespace SistemaGestionData.Context;

public class ProyectoFinalContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ProyectoFinalContext(DbContextOptions<ProyectoFinalContext> options, IConfiguration configuration)
    : base(options)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public DbSet<Producto> Productos { get; set; }

    public DbSet<Usuario> Usuarios { get; set; }

    public DbSet<Venta> Ventas { get; set; }

    public DbSet<ProductoVendido> ProductoVendido { get; set; }

    public DbSet<Login> Login { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Usuario userAdmin = new Usuario
        {
            Id = int.Parse(_configuration["Id"]),
            Nombre = _configuration["Nombre"],
            Apellido = _configuration["Apellido"],
            NombreUsuario = _configuration["NombreUsuario"],
            Email = _configuration["Email"],
            Contraseña = _configuration["Contraseña"]
        };

        Console.WriteLine(userAdmin.Email);

        modelBuilder.Entity<Usuario>().HasData(userAdmin);

        base.OnModelCreating(modelBuilder);
    }
}
