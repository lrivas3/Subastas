using System;
using System.Collections.Generic;

namespace Subastas.DAL.Models;

public partial class Permiso
{
    public int IdPermiso { get; set; }

    public string NombrePermiso { get; set; } = null!;

    public bool EstaActivo { get; set; }

    public int IdMenu { get; set; }

    public virtual Menu IdMenuNavigation { get; set; } = null!;

    public virtual ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();
}
