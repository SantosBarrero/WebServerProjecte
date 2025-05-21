using System;
using System.Collections.Generic;

namespace WebServiceProjecte.Models;

public partial class ProducteEncarrec
{
    public string CodiDeBarres { get; set; } = null!;

    public int EncarrecId { get; set; }

    public int? Quantitat { get; set; }

    public virtual Producte CodiDeBarresNavigation { get; set; } = null!;

    public virtual Encarrec Encarrec { get; set; } = null!;
}
