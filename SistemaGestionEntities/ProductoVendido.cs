using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaGestionEntities;

public class ProductoVendido
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Venta es requerido.")]
    public Venta Venta { get; set; }

    [Required(ErrorMessage = "Producto es requerido.")]
    public Producto Producto { get; set; }

    [Required(ErrorMessage = "Cantidad es requerido.")]
    [Range(0, double.MaxValue, ErrorMessage = "La Cantidad debe ser mayor o igual a 0.")]
    public int Cantidad { get; set; }

}
