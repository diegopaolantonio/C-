using SistemaGestionData.DataAccess;
using SistemaGestionEntities;

namespace SistemaGestionBussiness.Services;

public class UsuariosService
{
    private UsuariosDataAccess _usuariosDataAccess;

    public UsuariosService(UsuariosDataAccess usuariosDataAccess)
    {
        _usuariosDataAccess = usuariosDataAccess;
    }

    public async Task<List<Usuario>> GetUsuarios()
    {
        return await _usuariosDataAccess.GetUsuarios();
    }

    public Usuario GetOneUsuario(int id)
    {
        return _usuariosDataAccess.GetOneUsuario(id);
    }

    public async Task<Usuario> InsertUsuario(Usuario usuario)
    {
        return await _usuariosDataAccess.InsertUsuario(usuario);
    }

    public async Task UpdateUsuario(int id, Usuario usuario)
    {
        await _usuariosDataAccess.UpdateUsuario(id, usuario);
    }

    public async Task DeleteUsuario(int id)
    {
        await _usuariosDataAccess.DeleteUsuario(id);
    }

    public async Task<List<Usuario>> GetUsuariosBy(string filtro)
    {
        return await _usuariosDataAccess.GetUsuariosBy(filtro);
    }
}