using System;
using System.Collections.Generic;

namespace WebServiceProjecte.Models;

public partial class Stock
{
    public string CodiDeBarres { get; set; } = null!;

    public int SucursalId { get; set; }

    public int? Stock1 { get; set; }

    public virtual Producte CodiDeBarresNavigation { get; set; } = null!;

    public virtual Sucursal Sucursal { get; set; } = null!;
}
