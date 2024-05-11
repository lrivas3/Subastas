namespace Subastas.Domain;

public partial class Menu
{
    public int IdMenu { get; set; }

    public string NombreMenu { get; set; } = null!;

    public bool EstaActivo { get; set; }

    public int IdMenuPadre { get; set; }

    public virtual Menu IdMenuPadreNavigation { get; set; } = null!;

    public virtual ICollection<Menu> InverseIdMenuPadreNavigation { get; set; } = new List<Menu>();

    public virtual ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();
}
