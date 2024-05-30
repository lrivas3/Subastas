using System.ComponentModel.DataAnnotations;

namespace Subastas.Domain;

public partial class Cuenta
{
    public int IdCuenta { get; set; }
    [Display(Name = "Saldo de la cuenta")]
    [Range(0, double.MaxValue, ErrorMessage = "El saldo debe ser un número positivo")]
    [Required(ErrorMessage = "El saldo es obligatorio")]
    public decimal Saldo { get; set; }
    [Display(Name = "Estado de la cuenta")]
    public bool EstaActivo { get; set; } = true;

    public int IdUsuario { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
}
