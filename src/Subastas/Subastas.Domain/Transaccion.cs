namespace Subastas.Domain;

public partial class Transaccion
{
    public int IdTransaccion { get; set; }

    public decimal MontoTransaccion { get; set; }

    public DateOnly FechaTransaccion { get; set; }

    public bool EsAFavor { get; set; }

    public bool EstaActivo { get; set; }

    public int IdCuenta { get; set; }

    public virtual Cuenta IdCuentaNavigation { get; set; } = null!;
}
