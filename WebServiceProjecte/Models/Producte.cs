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

    public virtual ICollection<Encarrec> Encarrecs { get; set; } = new List<Encarrec>();

    public virtual ICollection<Sucursal> Sucursals { get; set; } = new List<Sucursal>();
}
