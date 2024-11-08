using SistemaGestionEntities;

namespace SistemaGestionUI.ClientServices;

public class ProductosVendidosService
{
    private readonly HttpClient _httpClient;

    public ProductosVendidosService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProductoVendido>> GetProductoVendido()
    {
        return await _httpClient.GetFromJsonAsync<List<ProductoVendido>>("");
    }

    public async Task<ProductoVendido?> GetOneProductoVendido(int id)
    {
        return await _httpClient.GetFromJsonAsync<ProductoVendido>($"{id}");
    }

    public async Task InsertProductoVendido(ProductoVendido productoVendido)
    {
        await _httpClient.PostAsJsonAsync("", productoVendido);
    }

    public async Task DeleteProductoVendido(int id)
    {
        await _httpClient.DeleteAsync($"{id}");
    }

}
