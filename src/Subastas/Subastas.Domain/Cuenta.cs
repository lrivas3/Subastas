namespace Subastas.Domain;

public partial class Cuenta
{
    public int IdCuenta { get; set; }

    public decimal Saldo { get; set; }

    public bool EstaActivo { get; set; } = true;

    public int IdUsuario { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
}
