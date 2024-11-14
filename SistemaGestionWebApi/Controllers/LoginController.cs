using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussiness.Services;
using SistemaGestionEntities;

namespace SistemaGestionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly LoginService _loginService;

        public LoginController(ILogger<LoginController> logger, LoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Login>>> GetLogin([FromQuery(Name = "filtro")] string? filtro)
        {

            if (filtro == null)
            {
                return await _loginService.GetLogin();
            }
            return await _loginService.GetLoginBy(filtro);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Login>> GetLogin([FromRoute(Name = "id")] int id)
        {
            _logger.LogInformation("Consultando por el login con id {id}", id);
            var usuario = await _loginService.GetOneLogin(id);
            if (usuario is null)
            {
                return NotFound();
            }
            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult<Login>> CrearLogin([FromBody] Login login)
        {
            var usuarioCreado = await _loginService.InsertLogin(login);
            return CreatedAtAction(nameof(GetLogin), new { id = usuarioCreado.Id }, login);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLogin([FromRoute(Name = "id")] int id)
        {
            await _loginService.DeleteLogin(id);
            return NoContent();
        }
    }
}