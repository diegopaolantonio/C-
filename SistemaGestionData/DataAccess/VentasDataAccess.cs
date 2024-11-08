using Microsoft.EntityFrameworkCore;
using SistemaGestionData.Context;
using SistemaGestionEntities;

namespace SistemaGestionData.DataAccess;

public class VentasDataAccess
{
    private readonly ProyectoFinalContext _context;

    public VentasDataAccess(ProyectoFinalContext context)
    {
        _context = context;
    }

    public async Task<List<Venta>> GetVentas()
    {
        return await _context.Ventas
            .Include(venta => venta.Usuario)
            .Include(venta => venta.Producto)
            .ToListAsync();
    }

    public async Task<Venta?> GetOneVenta(int id)
    {
        return await _context.Ventas
            .Include(venta => venta.Usuario)
            .Include(venta => venta.Producto)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Venta> InsertVenta(Venta venta)
    {
        await _context.Ventas.AddAsync(venta);
        await _context.SaveChangesAsync();

        return venta;
    }

    public async Task UpdateVenta(int id, Venta venta)
    {
        var ventaToUpdate = await GetOneVenta(id);

        if (ventaToUpdate != null)
        {
            ventaToUpdate.Usuario = venta.Usuario;
            ventaToUpdate.FechaVenta = new DateTime();
            ventaToUpdate.TotalVenta = venta.TotalVenta;
            _context.Ventas.Update(ventaToUpdate);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteVenta(int id)
    {
        var ventaToDelete = await GetOneVenta(id);
        if (ventaToDelete != null)
        {
            _context.Ventas.Remove(ventaToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
