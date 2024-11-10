using System;
using SistemaGestionData.DataAccess;
using SistemaGestionEntities;

namespace SistemaGestionBussiness.Services;

public class ProductosService
{
    private ProductosDataAccess _productosDataAccess;

    public ProductosService(ProductosDataAccess productosDataAccess)
    {
        _productosDataAccess = productosDataAccess;
    }

    public async Task<List<Producto>> GetProductos()
    {
        return await _productosDataAccess.GetProductos();
    }

    public async Task<List<Producto>> GetProductosBy(string filtro)
    {
        return await _productosDataAccess.GetProductosBy(filtro);
    }

    public async Task<Producto?> GetOneProducto(int id)
    {
        return await _productosDataAccess.GetOneProducto(id);
    }

    public async Task<Producto> InsertProducto(Producto producto)
    {
        return await _productosDataAccess.InsertProducto(producto);
    }

    public async Task UpdateProducto(int id, Producto producto)
    {
        await _productosDataAccess.UpdateProducto(id, producto);
    }

    public async Task DeleteProducto(int id)
    {
        await _productosDataAccess.DeleteProducto(id);
    }


    public async Task UpdateTotalProducto(int id)
    {
        var producto = await GetOneProducto(id);
        if (producto != null)
        {
            producto.TotalProducto = producto.Stock * producto.PrecioVenta;
            await _productosDataAccess.UpdateProducto(id, producto);
        }
    }

    public async Task UpdateTotalProductos()
    {
        var productos = await GetProductos();
        foreach (var producto in productos)
        {
            producto.TotalProducto = producto.Stock * producto.PrecioVenta;
            await _productosDataAccess.UpdateProducto(producto.Id, producto);
        }
    }

}