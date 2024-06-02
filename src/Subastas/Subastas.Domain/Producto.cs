using System.ComponentModel.DataAnnotations;

namespace Subastas.Domain;

public partial class Producto
{
    public int IdProducto { get; set; }
    [Display(Name = "Nombre del producto")]
    [Required(ErrorMessage = "El nombre del producto es obligatorio")]
    public string NombreProducto { get; set; } = null!;
    [Display(Name = "Descripción del producto")]
    [Required(ErrorMessage = "La descripción del producto es obligatorio")]
    public string DescripcionProducto { get; set; } = null!;

    public string ImagenProducto { get; set; } = null!;
    [Display(Name = "Estado del producto")]
    public bool EstaActivo { get; set; }
    [Display(Name = "Estado de lo subastado")]
    public bool EstaSubastado { get; set; }

    public virtual Subasta? Subasta { get; set; }
}
