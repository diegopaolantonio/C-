using Microsoft.AspNetCore.WebUtilities;
using SistemaGestionEntities;

namespace SistemaGestionUI.ClientServices;

public class LoginService
{
    private readonly HttpClient _httpClient;

    public LoginService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Login>> GetLogin()
    {
        return await _httpClient.GetFromJsonAsync<List<Login>>("");
    }

    public async Task<Login?> GetOneLogin(int id)
    {
        return await _httpClient.GetFromJsonAsync<Login>($"{id}");
    }

    public async Task InsertLogin(Login login)
    {
        await _httpClient.PostAsJsonAsync("", login);
    }

    public async Task DeleteLogin(int id)
    {
        await _httpClient.DeleteAsync($"{id}");
    }

    public async Task<List<Login?>> GetLoginBy(string filtro)
    {
        return await _httpClient.GetFromJsonAsync<List<Login>>(
            QueryHelpers.AddQueryString("", new Dictionary<string, string>() { { "filtro", filtro } }));
    }
}
