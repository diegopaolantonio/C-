using Microsoft.EntityFrameworkCore;
using SistemaGestionData.Context;
using SistemaGestionEntities;

namespace SistemaGestionData.DataAccess;

public class UsuariosDataAccess
{
    private readonly ProyectoFinalContext _context;

    public UsuariosDataAccess(ProyectoFinalContext context)
    {
        _context = context;
    }

    public async Task<List<Usuario>> GetUsuarios()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task<List<Usuario>> GetUsuariosBy(string filtro)
    {
        return await _context
            .Usuarios.Where(u =>
                u.Nombre.Contains(filtro) || u.Apellido.Contains(filtro) || u.Email.Contains(filtro)
            )
            .ToListAsync();
    }

    public async Task<Usuario> GetOneUsuario(int id)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Usuario> InsertUsuario(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task UpdateUsuario(int id, Usuario usuario)
    {
        var usuarioToUpdate = await GetOneUsuario(id);
        if (usuarioToUpdate != null)
        {
            usuarioToUpdate.Nombre = usuario.Nombre;
            usuarioToUpdate.Apellido = usuario.Apellido;
            usuarioToUpdate.NombreUsuario = usuario.NombreUsuario;
            usuarioToUpdate.Email = usuario.Email;
            usuarioToUpdate.Contraseña = usuario.Contraseña;
            _context.Usuarios.Update(usuarioToUpdate);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteUsuario(int id)
    {
        if(id != 1)
        {
            var usuarioToDelete = await GetOneUsuario(id);
            if (usuarioToDelete != null)
            {
                _context.Usuarios.Remove(usuarioToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
