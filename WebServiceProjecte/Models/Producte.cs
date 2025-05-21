using System;
using System.Collections.Generic;

namespace WebServiceProjecte.Models;

public partial class Producte
{
    public string CodiDeBarres { get; set; } = null!;

    public string? Nom { get; set; }

    public string? Imatge { get; set; }

    public string? Descripcio { get; set; }

    public decimal? Preu { get; set; }

    public string? Categoria { get; set; }

    public virtual ICollection<ProducteEncarrec> ProducteEncarrecs { get; set; } = new List<ProducteEncarrec>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
