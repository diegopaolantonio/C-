using Microsoft.EntityFrameworkCore;
using SistemaGestionData.Context;
using SistemaGestionEntities;

namespace SistemaGestionData.DataAccess;

public class ProductosVendidosDataAccess
{

    private readonly ProyectoFinalContext _context;

    public ProductosVendidosDataAccess(ProyectoFinalContext context)
    {
        _context = context;
    }

    public async Task<List<ProductoVendido>> GetProductoVendido()
    {
        return await _context.ProductoVendido.Include(p => p.Venta).ThenInclude(p => p.Usuario).Include(p => p.Producto).ToListAsync();
    }

    public List<ProductoVendido> GetProductoVendidoBy(string filtro)
    {
        return _context
            .ProductoVendido.Include(p => p.Venta).Include(p => p.Producto).Where(u =>
                u.Producto.Descripcion.Contains(filtro)
            )
            .ToList();
    }

    public async Task<ProductoVendido?> GetOneProductoVendido(int id)
    {
        return await _context.ProductoVendido.Include(p => p.Venta).Include(p => p.Producto).FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<ProductoVendido> InsertProductoVendido(ProductoVendido productoVendido)
    {
        await _context.ProductoVendido.AddAsync(productoVendido);
        await _context.SaveChangesAsync();
        return productoVendido;
    }

    public async Task DeleteProductoVendido(int id)
    {
        var productoVendidoToDelete = await GetOneProductoVendido(id);
        if (productoVendidoToDelete != null)
        {
            _context.ProductoVendido.Remove(productoVendidoToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
