﻿namespace Subastas.Domain;

public partial class Transaccione
{
    public int IdTransaccion { get; set; }

    public decimal MontoTransaccion { get; set; }

    public DateOnly FechaTransaccion { get; set; }

    public bool EsAFavor { get; set; }

    public bool EstaActivo { get; set; }

    public int IdCuenta { get; set; }

    public virtual Cuentum IdCuentaNavigation { get; set; } = null!;
}
