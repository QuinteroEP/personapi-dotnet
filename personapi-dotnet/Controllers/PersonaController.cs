using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using System.Linq;

namespace personapi_dotnet.Controllers
{
    public class PersonaController : Controller
    {
        private readonly ArqPerDbContext _context;

        public PersonaController(ArqPerDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var personas = await _context.Personas
                .Include(p => p.Telefonos)
                .ToListAsync();

            return View(personas);
        }

        public async Task<IActionResult> getTelefono()
        {
            var persona = await _context.Personas
                .Include(p => p.Telefonos)
                .FirstOrDefaultAsync();

            var telefonos = await _context.Telefonos
                .Where(t => t.Duenio.Equals(persona))
                .ToListAsync();

            foreach (var telefono in telefonos)
            {
                persona?.Telefonos.Add(telefono);
            }

            return View(await _context.Telefonos.ToListAsync());
        }

        public async Task<IActionResult> getUniversidad()
        {
            var persona = await _context.Personas
                .Include(p => p.Estudios)
                .FirstOrDefaultAsync();

            var universidad = await _context.Estudios
                .Where(u => u.CcPer.Equals(persona))
                .ToListAsync();

            foreach (var uni in universidad)
            {
                persona?.Estudios.Add(uni);
            }

            return View(await _context.Estudios.ToListAsync());
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var persona = await _context.Personas
                .Where(p => p.Cc.Equals(Id))
                .FirstOrDefaultAsync();

            _context.Estudios.RemoveRange(persona!.Estudios);
            _context.Personas.Remove(persona!);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
