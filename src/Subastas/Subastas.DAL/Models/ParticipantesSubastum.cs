using System;
using System.Collections.Generic;

namespace Subastas.DAL.Models;

public partial class ParticipantesSubastum
{
    public int IdSubasta { get; set; }

    public int IdUsuario { get; set; }

    public bool EstaActivo { get; set; }

    public virtual Subasta IdSubastaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
