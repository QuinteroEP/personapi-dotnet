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

 // keep existing delete by owner for compatibility
 public async Task delete(int id)
 {
 var item = await _context.Telefonos.FirstOrDefaultAsync(t => t.Duenio == id);
 if (item != null)
 {
 _context.Telefonos.Remove(item);
 await _context.SaveChangesAsync();
 }
 }

 // new: delete by phone number (primary key)
 public async Task deleteByNum(string num)
 {
 var item = await _context.Telefonos.FirstOrDefaultAsync(t => t.Num == num);
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
 // id param interpreted as duenio (owner id)
 return await _context.Telefonos.FirstAsync(t => t.Duenio == id);
 }

 // new: find by phone number
 public async Task<Telefono> findByNum(string num)
 {
 return await _context.Telefonos.FirstAsync(t => t.Num == num);
 }

 // Overload to support updating when primary key (Num) may change
 public async Task update(Telefono data, string originalNum)
 {
 // Try to find the entity by the original primary key if provided
 Telefono existing = null;
 if (!string.IsNullOrEmpty(originalNum))
 {
 existing = await _context.Telefonos.FirstOrDefaultAsync(t => t.Num == originalNum);
 }

 // If not found by originalNum, try to find by current Num
 if (existing == null && !string.IsNullOrEmpty(data?.Num))
 {
 existing = await _context.Telefonos.FirstOrDefaultAsync(t => t.Num == data.Num);
 }

 // Fallback: if still not found, try to find by owner (Duenio)
 if (existing == null)
 {
 existing = await _context.Telefonos.FirstOrDefaultAsync(t => t.Duenio == data.Duenio);
 }

 if (existing == null)
 {
 // nothing to update
 return;
 }

 // If the primary key (Num) changed, remove the old entity and insert the new one
 if (!string.Equals(existing.Num, data.Num))
 {
 _context.Telefonos.Remove(existing);
 _context.Telefonos.Add(data);
 }
 else
 {
 // Update scalar properties
 existing.Oper = data.Oper;
 existing.Duenio = data.Duenio;
 }

 await _context.SaveChangesAsync();
 }

 // Backwards-compatible update method
 public async Task update(Telefono data)
 {
 await update(data, null);
 }
 }
}
