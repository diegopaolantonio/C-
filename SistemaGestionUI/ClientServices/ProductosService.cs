using Microsoft.AspNetCore.WebUtilities;
using SistemaGestionEntities;

namespace SistemaGestionUI.ClientServices;

public class ProductosService
{
    private readonly HttpClient _httpClient;

    public ProductosService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Producto>> GetProductos()
    {
        return await _httpClient.GetFromJsonAsync<List<Producto>>("");
    }

    public async Task<List<Producto>?> GetProductosBy(string filtro)
    {
        return await _httpClient.GetFromJsonAsync<List<Producto>>(
            QueryHelpers.AddQueryString("", new Dictionary<string, string>() { { "filtro", filtro } }));
    }

    public async Task<Producto?> GetOneProducto(int id)
    {
        return await _httpClient.GetFromJsonAsync<Producto>($"{id}");
    }

    public async Task InsertProducto(Producto producto)
    {
        await _httpClient.PostAsJsonAsync("", producto);
    }

    public async Task UpdateProducto(int id, Producto producto)
    {
        await _httpClient.PutAsJsonAsync($"{id}", producto);
    }

    public async Task DeleteProducto(int id)
    {
        await _httpClient.DeleteAsync($"{id}");
    }


    public Task UpdateTotalProductos()
    {
        return _httpClient.PutAsync("fix-total", null);
    }
}
