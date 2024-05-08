using System;
using System.Collections.Generic;

namespace Subastas.Models;

public partial class UsuarioRol
{
    public int IdUsuario { get; set; }

    public int IdRol { get; set; }

    public bool EstaActivo { get; set; }

    public virtual Role IdRolNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
