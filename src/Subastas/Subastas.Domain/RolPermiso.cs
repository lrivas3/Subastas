namespace Subastas.Domain;

public partial class RolPermiso
{
    public int IdRol { get; set; }

    public int IdPermiso { get; set; }

    public bool EstaActivo { get; set; }

    public virtual Permiso IdPermisoNavigation { get; set; } = null!;

    public virtual Role IdRolNavigation { get; set; } = null!;
}
