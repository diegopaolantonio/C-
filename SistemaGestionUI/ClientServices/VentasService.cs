using SistemaGestionEntities;
using SistemaGestionUI.Components.Pages.Ventas;

namespace SistemaGestionUI.ClientServices;

public class VentasService
{
    private readonly HttpClient _httpClient;

    public VentasService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Venta>> GetVentas()
    {
        List<Venta>? ventas = await _httpClient.GetFromJsonAsync<List<Venta>>("");
        return ventas;
    }

    public async Task<Venta?> GetOneVenta(int id)
    {
        return await _httpClient.GetFromJsonAsync<Venta>($"{id}");
    }

    public async Task InsertVenta(IngresarVenta ingresarVenta)
    {
        await _httpClient.PostAsJsonAsync("", ingresarVenta);
    }

    public async Task DeleteVenta(int id)
    {
        await _httpClient.DeleteAsync($"{id}");
    }

}
