using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussiness.Services;
using SistemaGestionEntities;

namespace SistemaGestionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly UsuariosService _usuariosService;

        public UsuariosController(ILogger<UsuariosController> logger, UsuariosService usuariosService)
        {
            _logger = logger;
            _usuariosService = usuariosService;
        }

        [HttpGet(Name = "Get Usuarios")]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios([FromQuery(Name = "filtro")] string? filtro)
        {
            if (filtro == null)
            {
                return await _usuariosService.GetUsuarios();
            }
            return await _usuariosService.GetUsuariosBy(filtro);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario([FromRoute(Name = "id")] int id)
        {
            _logger.LogInformation("Consultando por el producto con id {id}", id);
            var usuario = await _usuariosService.GetOneUsuario(id);
            if (usuario is null)
            {
                return NotFound();
            }
            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> CrearUsuario([FromBody] Usuario usuario)
        {
            var usuarioCreado = await _usuariosService.InsertUsuario(usuario);
            return CreatedAtAction(nameof(GetUsuario), new { id = usuarioCreado.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ModificarUsuario([FromRoute(Name = "id")] int id, [FromBody] Usuario usuario)
        {
            await _usuariosService.UpdateUsuario(id, usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsuario([FromRoute(Name = "id")] int id)
        {
            if (id != 1)
            {
                await _usuariosService.DeleteUsuario(id);
                return NoContent();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}