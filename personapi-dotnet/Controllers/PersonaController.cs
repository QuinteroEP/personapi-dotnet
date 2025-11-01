using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;

using System.Linq;

namespace personapi_dotnet.Controllers
{
    public class PersonaController : Controller
    {

        private readonly PersonaRepository _repo;
        public PersonaController(PersonaRepository repository)
        {
            _repo = repository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _repo.findAll());
        }

        public async Task<IActionResult> Edit(int id)
        {

            return View("FormularioEditar");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async void Add(Persona persona)
        {
            await _repo.create(persona);
        }

        public async Task<IActionResult> getTelefono()
        {
            return View(await _repo.getTelefonos());
        }

        public async Task<IActionResult> getUniversidad()
        {
            return View(await _repo.getUniversidad());
        }

        public async void Delete(int Id)
        {
            await _repo.delete(Id);

            RedirectToAction(nameof(Index));
        }
    }
}
