using System;
using System.Collections.Generic;

namespace WebServiceProjecte.Models;

public partial class Comerç
{
    public int ComerçId { get; set; }

    public string? Nom { get; set; }

    public string? Telefon { get; set; }

    public string? Email { get; set; }

    public string? Nif { get; set; }

    public virtual ICollection<Sucursal> Sucursals { get; set; } = new List<Sucursal>();

    public virtual ICollection<Usuari> Usuaris { get; set; } = new List<Usuari>();
}
