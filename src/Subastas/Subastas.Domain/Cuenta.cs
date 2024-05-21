namespace Subastas.Domain;

public partial class Cuenta
{
    public int IdCuenta { get; set; }

    public decimal Saldo { get; set; }

    public bool EstaActivo { get; set; }

    public int IdUsuario { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
}
