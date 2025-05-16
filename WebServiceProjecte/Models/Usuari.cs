using System;
using System.Collections.Generic;

namespace WebServiceProjecte.Models;

public partial class Usuari
{
    public int UsuId { get; set; }

    public string? NomUsuari { get; set; }

    public string? Correu { get; set; }

    public string? Contrasenya { get; set; }

    public string? Rol { get; set; }

    public int? SucursalId { get; set; }

    public int? ComerçId { get; set; }

    public virtual Comerç? Comerç { get; set; }

    public virtual ICollection<Encarrec> Encarrecs { get; set; } = new List<Encarrec>();

    public virtual Sucursal? Sucursal { get; set; }
}
