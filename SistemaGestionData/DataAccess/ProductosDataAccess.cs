using Microsoft.EntityFrameworkCore;
using SistemaGestionData.Context;
using SistemaGestionEntities;

namespace SistemaGestionData.DataAccess;

public class ProductosDataAccess
{
    private readonly ProyectoFinalContext _context;

    public ProductosDataAccess(ProyectoFinalContext context)
    {
        _context = context;
    }

    public async Task<List<Producto>> GetProductos()
    {
        return await _context.Productos.ToListAsync();
    }

    public async Task<List<Producto>> GetProductosBy(string filtro)
    {
        return await _context.Productos.Where(p => p.Categoria.Contains(filtro)).ToListAsync();
    }

    public async Task<Producto?> GetOneProducto(int id)
    {
        return await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Producto> InsertProducto(Producto producto)
    {
        await _context.Productos.AddAsync(producto);
        await _context.SaveChangesAsync();
        return producto;
    }

    public async Task UpdateProducto(int id, Producto producto)
    {
        var productoToUpdate = await GetOneProducto(id);
        if (productoToUpdate != null)
        {
            productoToUpdate.Descripcion = producto.Descripcion;
            productoToUpdate.Categoria = producto.Categoria;
            productoToUpdate.Stock = producto.Stock;
            productoToUpdate.PrecioCompra = producto.PrecioCompra;
            productoToUpdate.PrecioVenta = producto.PrecioVenta;
            productoToUpdate.TotalProducto = producto.TotalProducto;
            _context.Productos.Update(productoToUpdate);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteProducto(int id)
    {
        var productoToDelete = await GetOneProducto(id);
        if (productoToDelete != null)
        {
            _context.Productos.Remove(productoToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
