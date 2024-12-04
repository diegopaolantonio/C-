using System;
using SistemaGestionData.DataAccess;
using SistemaGestionEntities;

namespace SistemaGestionBussiness.Services;

public class LoginService
{
    private LoginDataAccess _loginDataAccess;

    public LoginService(LoginDataAccess loginDataAccess)
    {
        _loginDataAccess = loginDataAccess;
    }

    public async Task<List<Login>> GetLogin()
    {
        return await _loginDataAccess.GetLogin();
    }

    public async Task<Login?> GetOneLogin(int id)
    {
        return await _loginDataAccess.GetOneLogin(id);
    }

    public async Task<Login> InsertLogin(Login login)
    {
        return await _loginDataAccess.InsertLogin(login);
    }

    public async Task DeleteLogin(int id)
    {
        await _loginDataAccess.DeleteLogin(id);
    }

    //public async Task<List<Login>> GetLoginByEmail(string token)
    //{
    //    return await _loginDataAccess.GetLoginByEmail(token);
    //}

    public async Task<List<Login>> GetLoginByToken(string token)
    {
        return await _loginDataAccess.GetLoginByToken(token);
    }
}