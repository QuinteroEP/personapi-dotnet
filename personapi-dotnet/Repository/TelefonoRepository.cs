using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interface;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repository
{
 public class TelefonoRepository : EntityInterface<Telefono>
 {
 private readonly ArqPerDbContext _context;

 public TelefonoRepository(ArqPerDbContext context)
 {
 _context = context;
 }

 public async Task create(Telefono data)
 {
 _context.Telefonos.Add(data);
 await _context.SaveChangesAsync();
 }

 public async Task delete(int id)
 {
 var item = await _context.Telefonos.FirstOrDefaultAsync(t => t.Duenio == id);
 if (item != null)
 {
 _context.Telefonos.Remove(item);
 await _context.SaveChangesAsync();
 }
 }

 public async Task<List<Telefono>> findAll()
 {
 return await _context.Telefonos.Include(t => t.DuenioNavigation).ToListAsync();
 }

 public async Task<Telefono> findById(int id)
 {
 // id param interpreted as duenio
 return await _context.Telefonos.FirstAsync(t => t.Duenio == id);
 }

 public async Task update(Telefono data)
 {
 _context.Telefonos.Update(data);
 await _context.SaveChangesAsync();
 }
 }
}
