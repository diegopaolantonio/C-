using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussiness.Services;
using SistemaGestionEntities;

namespace SistemaGestionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ILogger<ProductosController> _logger;
        private readonly ProductosService _productosService;

        public ProductosController(ILogger<ProductosController> logger, ProductosService productosService)
        {
            _logger = logger;
            _productosService = productosService;
        }

        [HttpGet(Name = "Get Productos")]
        public async Task<ActionResult<List<Producto>>> GetProductos([FromQuery(Name = "filtro")] string? filtro)
        {
            if (filtro == null)
            {
                return await _productosService.GetProductos();
            }
            return await _productosService.GetProductosBy(filtro);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            _logger.LogInformation("Consultando por el producto con id {id}", id);
            var producto = await _productosService.GetOneProducto(id);
            if (producto is null)
            {
                return NotFound();
            }
            return producto;
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> CrearProducto([FromBody] Producto producto)
        {
            var productoCreado = await _productosService.InsertProducto(producto);
            return CreatedAtAction(nameof(GetProducto), new { id = productoCreado.Id }, producto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ModificarProducto([FromRoute(Name = "id")] int id, [FromBody] Producto producto)
        {
            await _productosService.UpdateProducto(id, producto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProducto([FromRoute(Name = "id")] int id)
        {
            await _productosService.DeleteProducto(id);
            return NoContent();
        }

        [HttpPut("fix-total")]
        public async Task<ActionResult> FixTotalProductos()
        {
            await _productosService.UpdateTotalProductos();
            return NoContent();
        }

    }
}