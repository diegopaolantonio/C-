using SistemaGestionData.DataAccess;
using SistemaGestionEntities;

namespace SistemaGestionBussiness.Services;

public class ProductosVendidosService
{
    private ProductosVendidosDataAccess _productosVendidosDataAccess;

    public ProductosVendidosService(ProductosVendidosDataAccess productosVendidosDataAccess)
    {
        _productosVendidosDataAccess = productosVendidosDataAccess;
    }

    public async Task<List<ProductoVendido>> GetProductoVendido()
    {
        return await _productosVendidosDataAccess.GetProductoVendido();
    }

    public async Task<ProductoVendido?> GetOneProductoVendido(int id)
    {
        return await _productosVendidosDataAccess.GetOneProductoVendido(id);
    }

    public List<ProductoVendido> GetProductoVendidoBy(string filtro)
    {
        return _productosVendidosDataAccess.GetProductoVendidoBy(filtro);
    }

    public async Task<ProductoVendido> InsertProductoVendido(ProductoVendido productoVendido)
    {
        return await _productosVendidosDataAccess.InsertProductoVendido(productoVendido);
    }

    public async Task DeleteProductoVendido(int id)
    {
        await _productosVendidosDataAccess.DeleteProductoVendido(id);
    }
}
