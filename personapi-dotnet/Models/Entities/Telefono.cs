using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace personapi_dotnet.Models.Entities;

[Table("telefono", Schema = "arq_per_db")]
public partial class Telefono
{
    public string Num { get; set; } = null!;

    public string Oper { get; set; } = null!;

    public int Duenio { get; set; }

    public virtual Persona DuenioNavigation { get; set; } = null!;
}
