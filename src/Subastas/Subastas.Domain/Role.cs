namespace Subastas.Domain;

public partial class Role
{
    public int IdRol { get; set; }

    public string NombreRol { get; set; } = null!;

    public bool EstaActivo { get; set; }

    public virtual ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();

    public virtual ICollection<UsuarioRol> UsuarioRols { get; set; } = new List<UsuarioRol>();
}
