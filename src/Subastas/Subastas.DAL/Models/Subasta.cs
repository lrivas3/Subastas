using System;
using System.Collections.Generic;

namespace Subastas.DAL.Models;

public partial class Subasta
{
    public int IdSubasta { get; set; }

    public string TituloSubasta { get; set; } = null!;

    public decimal MontoInicial { get; set; }

    public DateOnly FechaSubasta { get; set; }

    public bool EstaActivo { get; set; }

    public string ImagenSubasta { get; set; } = null!;

    public int? IdUsuario { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<ParticipantesSubastum> ParticipantesSubasta { get; set; } = new List<ParticipantesSubastum>();

    public virtual ICollection<Puja> Pujas { get; set; } = new List<Puja>();
}
