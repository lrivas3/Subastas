namespace Subastas.Domain;

public partial class Cuentum
{
    public int IdCuenta { get; set; }

    public decimal Saldo { get; set; }

    public int IdUsuario { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Transaccione> Transacciones { get; set; } = new List<Transaccione>();
}
