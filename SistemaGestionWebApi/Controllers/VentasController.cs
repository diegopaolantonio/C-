using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussiness.Services;
using SistemaGestionEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SistemaGestionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly ILogger<VentasController> _logger;
        private readonly VentasService _ventasService;
        private readonly UsuariosService _usuariosService;
        private readonly ProductosService _productosService;
        private readonly ProductosVendidosService _productosVendidosService;

        public VentasController(
            ILogger<VentasController> logger, 
            VentasService ventasService,
            UsuariosService usuariosService,
            ProductosService productosService,
            ProductosVendidosService productosVendidosService
            )
        {
            _logger = logger;
            _ventasService = ventasService;
            _usuariosService = usuariosService;
            _productosService = productosService;
            _productosVendidosService = productosVendidosService;
    }

        [HttpGet(Name = "Get Venta")]
        public async Task<ActionResult<List<Venta>>> GetVentas()
        {
            Usuario usuario = new Usuario();
            return await _ventasService.GetVentas();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Venta>> GetVenta(int id)
        {
            _logger.LogInformation("Consultando por la venta con id {id}", id);
            var venta = await _ventasService.GetOneVenta(id);
            if (venta is null)
            {
                return NotFound();
            }
            return venta;
        }

        [HttpPost(Name = "Post Venta")]
        public async Task<ActionResult<Venta>> CrearVenta([FromBody] IngresarVenta IngresarVenta)
        {
            Venta venta = new Venta();
            List<Producto> productos = new List<Producto>();
            decimal totalVenta = 0;

            var usuario = await _usuariosService.GetOneUsuario(IngresarVenta.UsuarioId);

            if (usuario is null) { 
                return NotFound();
            }

            if(IngresarVenta.ProductoId.Count != IngresarVenta.Cantidad.Count)
            {
                return BadRequest();
            }

            foreach (var (productoId, cantidad) in IngresarVenta.ProductoId.Zip(IngresarVenta.Cantidad))
            {
                var producto = await _productosService.GetOneProducto(productoId);
                Console.WriteLine(producto.Descripcion);
                productos.Add(producto);
                totalVenta += producto.PrecioVenta * cantidad;
            }

            venta.TotalVenta = totalVenta;
            venta.FechaVenta = new DateTime();
            venta.Usuario = usuario;
            venta.Producto = productos;
            venta.Cantidad = IngresarVenta.Cantidad;

            var ventaCreado = await _ventasService.InsertVenta(venta);

            foreach(var (producto, cantidad) in ventaCreado.Producto.Zip(ventaCreado.Cantidad))
            {
                ProductoVendido productoVendido = new ProductoVendido();
                productoVendido.Venta = ventaCreado;
                productoVendido.Producto = producto;
                productoVendido.Cantidad = cantidad;

                await _productosVendidosService.InsertProductoVendido(productoVendido);
            }

            return CreatedAtAction(nameof(GetVenta), new { id = ventaCreado.Id }, ventaCreado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ModificarVenta([FromRoute(Name = "id")] int id, [FromBody] Venta venta)
        {
            await _ventasService.UpdateVenta(id, venta);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVenta([FromRoute(Name = "id")] int id)
        {
            await _ventasService.DeleteVenta(id);
            return NoContent();
        }

    }
}