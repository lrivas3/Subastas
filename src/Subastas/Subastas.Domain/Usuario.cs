using System.ComponentModel.DataAnnotations;

namespace Subastas.Domain;

public partial class Usuario
{
    public int IdUsuario { get; set; }
    [Display(Name = "Nombre del usuario")]
    [Required(ErrorMessage = "El nombre del usuario es obligatorio")]
    public string NombreUsuario { get; set; } = null!;
    [Display(Name = "Apellido del usuario")]
    [Required(ErrorMessage = "El Apellido del usuario es obligatorio")]
    public string ApellidoUsuario { get; set; } = null!;
    [Display(Name = "Correo del usuario")]
    [Required(ErrorMessage = "El correo del usuario es obligatorio")]
    public string CorreoUsuario { get; set; } = null!;
    [Display(Name = "Contraseña del usuario")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 20 caracteres.")]
    public string? Password { get; set; } = null!;
    [Display(Name = "Estado del usuario")]
    public bool EstaActivo { get; set; } = true;

    public virtual Cuenta? Cuentum { get; set; }

    public virtual ICollection<ParticipantesSubasta> ParticipantesSubasta { get; set; } = new List<ParticipantesSubasta>();

    public virtual ICollection<Puja> Pujas { get; set; } = new List<Puja>();

    public virtual ICollection<Subasta> Subasta { get; set; } = new List<Subasta>();

    public virtual ICollection<UsuarioRol> UsuarioRols { get; set; } = new List<UsuarioRol>();
}
