using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace personapi_dotnet.Models.Entities;

[Table("persona", Schema = "arq_per_db")]
public partial class Estudio
{
    public int IdProf { get; set; }

    public int CcPer { get; set; }

    public DateOnly? Fecha { get; set; }

    public string? Univer { get; set; }

    public virtual Persona CcPerNavigation { get; set; } = null!;

    public virtual Profesion IdProfNavigation { get; set; } = null!;
}
