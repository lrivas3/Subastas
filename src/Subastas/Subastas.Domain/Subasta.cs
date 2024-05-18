namespace Subastas.Domain;

public partial class Subasta
{
    public int IdSubasta { get; set; }

    public string TituloSubasta { get; set; } = null!;

    public decimal MontoInicial { get; set; }

    public DateOnly FechaSubasta { get; set; }

    public DateOnly FechaSubastaFin { get; set; }

    public bool Finalizada { get; set; }

    public bool EstaActivo { get; set; }

    public int? IdUsuario { get; set; }

    public int IdProducto { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<ParticipantesSubastum> ParticipantesSubasta { get; set; } = new List<ParticipantesSubastum>();

    public virtual ICollection<Puja> Pujas { get; set; } = new List<Puja>();
}
