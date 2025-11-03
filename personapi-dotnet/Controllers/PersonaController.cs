using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Interface;

namespace personapi_dotnet.Controllers
{
    [Route("[controller]")]
    public class PersonaController : Controller
    {

        private readonly IPersonaRepository _repo;
        public PersonaController(IPersonaRepository repository)
        {
            _repo = repository;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _repo.findAll());
        }

        [HttpGet("Info")]
        public async Task<IActionResult> Info(int id)
        {
            var persona = await _repo.findById(id);
            if (persona == null)
                return NotFound();
            return View("info", persona);
        }

        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var persona = await _repo.findById(id);
            return View("FormularioEditar", persona);
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(Persona persona)
        {
            if (!ModelState.IsValid)
                return View(persona);

            await _repo.update(persona);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Add")]
        public IActionResult Add()
        {
            return View("FormularioCrear");
        }

        [HttpPost("Add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return View("FormularioCrear", persona);
            }

            await _repo.create(persona);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Telefonos")]
        public async Task<IActionResult> getTelefonos()
        {
            return View(await _repo.getTelefonos());
        }

        [HttpGet("Universidades")]
        public async Task<IActionResult> getUniversidades()
        {
            return View(await _repo.getUniversidad());
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            await _repo.delete(Id);

            return RedirectToAction(nameof(Index));
        }
    }
}
