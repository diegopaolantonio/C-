using Microsoft.EntityFrameworkCore;
using SistemaGestionData.Context;
using SistemaGestionEntities;

namespace SistemaGestionData.DataAccess;

public class LoginDataAccess
{
    private readonly ProyectoFinalContext _context;

    public LoginDataAccess(ProyectoFinalContext context)
    {
        _context = context;
    }

    public async Task<List<Login>> GetLogin()
    {
        return await _context.Login.ToListAsync();
    }

    public async Task<List<Login>> GetLoginBy(string filtro)
    {
        return await _context
            .Login.Where(u =>
                u.Email.Contains(filtro)
            )
            .ToListAsync();
    }

    public async Task<Login?> GetOneLogin(int id)
    {
        return await _context.Login.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Login> InsertLogin(Login login)
    {
        await _context.Login.AddAsync(login);
        await _context.SaveChangesAsync();
        return login;
    }

    public async Task DeleteLogin(int id)
    {
        var loginToDelete = await GetOneLogin(id);
        if (loginToDelete != null)
        {
            _context.Login.Remove(loginToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
