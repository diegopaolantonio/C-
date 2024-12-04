using Blazored.LocalStorage;
using Microsoft.AspNetCore.WebUtilities;
using SistemaGestionEntities;

namespace SistemaGestionUI.ClientServices;

public class LoginService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public LoginService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<List<Login>> GetLogin()
    {
        return await _httpClient.GetFromJsonAsync<List<Login>>("");
    }

    public async Task<Login?> GetOneLogin(int id)
    {
        return await _httpClient.GetFromJsonAsync<Login>($"{id}");
    }

    public async Task<string?> InsertLogin(Login login)
    {
        var response = await _httpClient.PostAsJsonAsync("", login);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<Login>();

        return result?.token;
        }

        return null;
    }

    public async Task DeleteLogin(int id)
    {
        await _httpClient.DeleteAsync($"{id}");
    }

    public async Task<List<Login?>> GetLoginBy(string token)
    {
        return await _httpClient.GetFromJsonAsync<List<Login>>(
            QueryHelpers.AddQueryString("", new Dictionary<string, string>() { { "token", token } }));
    }
    public async Task<string> GetTokenAsync()
    {
        return await _localStorage.GetItemAsync<string>("authToken");
    }
}
