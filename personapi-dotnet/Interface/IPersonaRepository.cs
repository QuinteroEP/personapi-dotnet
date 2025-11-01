using System.Collections.Generic;
using System.Threading.Tasks;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interface
{
 public interface IPersonaRepository : EntityInterface<Persona>
 {
 Task<List<Telefono>> getTelefonos();
 Task<List<Estudio>> getUniversidad();
 }
}
