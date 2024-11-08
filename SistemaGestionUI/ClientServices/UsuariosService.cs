using Microsoft.AspNetCore.WebUtilities;
using SistemaGestionEntities;

namespace SistemaGestionUI.ClientServices;

public class UsuariosService
{
    private readonly HttpClient _httpClient;

    public UsuariosService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Usuario>> GetUsuarios()
    {
        return await _httpClient.GetFromJsonAsync<List<Usuario>>("");
    }

    public async Task<Usuario?> GetOneUsuario(int id)
    {
        return await _httpClient.GetFromJsonAsync<Usuario>($"{id}");
    }

    public async Task InsertUsuario(Usuario usuario)
    {
        await _httpClient.PostAsJsonAsync("", usuario);
    }

    public async Task UpdateUsuario(int id, Usuario usuario)
    {
        await _httpClient.PutAsJsonAsync($"{id}", usuario);
    }

    public async Task DeleteUsuario(int id)
    {
        await _httpClient.DeleteAsync($"{id}");
    }

    public async Task<List<Usuario>> GetUsuariosBy(string filtro)
    {
        return await _httpClient.GetFromJsonAsync<List<Usuario>>(
            QueryHelpers.AddQueryString("", new Dictionary<string, string>() { { "filtro", filtro } }));
    }
}
