using System;
using System.Collections.Generic;

namespace Subastas.DAL.Models;

public partial class Puja
{
    public int IdPuja { get; set; }

    public decimal MontoPuja { get; set; }

    public DateOnly FechaPuja { get; set; }

    public int IdSubasta { get; set; }

    public int IdUsuario { get; set; }

    public virtual Subasta IdSubastaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
