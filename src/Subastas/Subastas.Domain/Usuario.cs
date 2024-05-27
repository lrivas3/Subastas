namespace Subastas.Domain;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string ApellidoUsuario { get; set; } = null!;

    public string CorreoUsuario { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool EstaActivo { get; set; } = true;

    public virtual Cuenta? Cuentum { get; set; }

    public virtual ICollection<ParticipantesSubasta> ParticipantesSubasta { get; set; } = new List<ParticipantesSubasta>();

    public virtual ICollection<Puja> Pujas { get; set; } = new List<Puja>();

    public virtual ICollection<Subasta> Subasta { get; set; } = new List<Subasta>();

    public virtual ICollection<UsuarioRol> UsuarioRols { get; set; } = new List<UsuarioRol>();
}
