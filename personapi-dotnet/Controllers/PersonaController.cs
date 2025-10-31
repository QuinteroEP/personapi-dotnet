using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;

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
            return View(await _context.Personas.ToListAsync());
        }
    }
}
