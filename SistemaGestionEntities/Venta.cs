using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaGestionEntities;

public class Venta
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Usuario es requerido.")]
    public Usuario Usuario { get; set; }

    [Required(ErrorMessage = "Producto es requerido.")]
    public List<Producto> Producto { get; set; }

    [Required(ErrorMessage = "Cantidad es requerido.")]
    public List<int> Cantidad { get; set; }

    [Required(ErrorMessage = "FechaVenta es requerido.")]
    public DateTime FechaVenta { get; set; }

    [Required(ErrorMessage = "TotalVenta es requerido.")]
    [Range(0, double.MaxValue, ErrorMessage = "La TotalVenta debe ser mayor o igual a 0.")]
    public decimal TotalVenta { get; set; }

}

public class IngresarVenta
{
    public int UsuarioId { get; set; }

    public List<int> ProductoId { get; set; }

    public List<int> Cantidad { get; set; }
}