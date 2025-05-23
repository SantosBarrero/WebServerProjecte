﻿using System;
using System.Collections.Generic;

namespace WebServiceProjecte.Models;

public partial class Sucursal
{
    public int SucursalId { get; set; }

    public string? Direccio { get; set; }

    public int? ComerçId { get; set; }

    public virtual Comerç? Comerç { get; set; }

    public virtual ICollection<Encarrec> Encarrecs { get; set; } = new List<Encarrec>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    public virtual ICollection<Usuari> Usuaris { get; set; } = new List<Usuari>();
}
