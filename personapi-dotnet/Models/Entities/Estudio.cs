using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace personapi_dotnet.Models.Entities;

[Table("estudios", Schema = "arq_per_db")]
public partial class Estudio
{
    [Key, Column(Order = 0)]
    public int IdProf { get; set; }
    
    [Key, Column(Order = 1)]
    public int CcPer { get; set; }

    public DateOnly? Fecha { get; set; }

    public string? Univer { get; set; }

    public virtual Persona CcPerNavigation { get; set; } = null!;

    public virtual Profesion IdProfNavigation { get; set; } = null!;
}
