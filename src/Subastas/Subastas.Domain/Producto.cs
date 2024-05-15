namespace Subastas.Domain;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public string DescripcionProducto { get; set; } = null!;

    public string ImagenProducto { get; set; } = null!;

    public bool EstaActivo { get; set; }

    public bool EstaSubastado { get; set; }

    public virtual Subasta? Subasta { get; set; }
}
