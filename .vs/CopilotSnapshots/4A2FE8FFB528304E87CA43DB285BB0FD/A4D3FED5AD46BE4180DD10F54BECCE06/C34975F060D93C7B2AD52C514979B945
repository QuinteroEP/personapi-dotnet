using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace personapi_dotnet.Interface
{
    public interface EntityInterface<T>
    {
        Task<List<T>> findAll();
        Task create(T data);
        Task<T> findById(int id);
        Task delete(int id);
        Task update(T data);
    }
}
