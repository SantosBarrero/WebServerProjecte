using System;
using System.Collections.Generic;

namespace WebServiceProjecte.Models;

public partial class Encarrec
{
    public int EncarrecId { get; set; }

    public decimal? PreuTotal { get; set; }

    public DateOnly? Data { get; set; }

    public bool Pagat { get; set; }

    public string? Estat { get; set; }

    public int? SucursalId { get; set; }    

    public int? UsuId { get; set; }

    public virtual Sucursal? Sucursal { get; set; }

    public virtual Usuari? Usu { get; set; }

    public virtual ICollection<Producte> CodiDeBarres { get; set; } = new List<Producte>();
}
