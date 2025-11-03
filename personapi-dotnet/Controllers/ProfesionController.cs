using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;

namespace personapi_dotnet.Controllers
{
    [Route("[controller]")]
    public class ProfesionController : Controller
    {
        private readonly ProfesionRepository _repo;
        public ProfesionController(ProfesionRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _repo.findAll());
        }

        [HttpGet("Add")]
        public IActionResult Add()
        {
            return View("FormularioCrear");
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(Profesion profesion)
        {
            if (!ModelState.IsValid)
                return View("FormularioCrear", profesion);

            await _repo.create(profesion);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _repo.findById(id);
            return View("FormularioEditar", item);
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Profesion profesion)
        {
            if (!ModelState.IsValid)
                return View("FormularioEditar", profesion);

            await _repo.update(profesion);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
