using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace personapi_dotnet.Models.Entities;

[Table("profesion", Schema = "arq_per_db")]
public partial class Profesion
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string? Des { get; set; }

    public virtual ICollection<Estudio> Estudios { get; set; } = new List<Estudio>();
}
