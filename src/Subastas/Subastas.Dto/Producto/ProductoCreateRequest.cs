using System.ComponentModel.DataAnnotations;

namespace Subastas.Dto.Producto;

public class ProductoCreateRequest
{
    public string NombreProducto { get; set; } = null!;

    [MaxLength(500)]
    public string DescripcionProducto { get; set; } = null!;

    public bool EstaActivo { get; set; }
}