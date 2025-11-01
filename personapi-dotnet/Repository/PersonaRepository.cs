using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interface;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repository
{
    public class PersonaRepository : EntityInterface<Persona>
    {
        private readonly ArqPerDbContext _context;

        public PersonaRepository(ArqPerDbContext context)
        {
            _context = context;
        }

        public async Task create(Persona data)
        {
            _context.Add(data);
            await _context.SaveChangesAsync();
        }

        public async Task delete(int id)
        {
            var estudios = await _context.Estudios
                .Where(e => e.CcPer == id)
                .ToListAsync();

            _context.Estudios.RemoveRange(estudios);

            // Step 2: Delete all Telefonos for that Persona
            var telefonos = await _context.Telefonos
                .Where(t => t.Duenio == id)
                .ToListAsync();

            _context.Telefonos.RemoveRange(telefonos);

            // Step 3: Delete the Persona itself
            var persona = await _context.Personas
                .FirstOrDefaultAsync(p => p.Cc == id);

             _context.Personas.Remove(persona!);
             await _context.SaveChangesAsync();
        }

        public async Task<List<Persona>> findAll()
        {
            var personas = await _context.Personas
                .Include(p => p.Telefonos)
                .Include(p => p.Estudios)
                .ToListAsync();
            return personas;
        }

        public async Task<Persona> findById(int id)
        {
            var persona = await _context.Personas
                .Include(p => p.Telefonos)
                .Include(p => p.Estudios)
                .Where(p => p.Cc == id)
                .FirstAsync();

            if (persona == null)
            {
                throw new Exception("Persona not found");
            }

            return persona;
        }

        public async Task update(Persona data)
        {
            _context.Personas.Update(data);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Telefono>> getTelefonos()
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

            return await _context.Telefonos.ToListAsync();
        }

        public async Task<List<Estudio>> getUniversidad()
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

            return await _context.Estudios.ToListAsync();
        }
    }
}
