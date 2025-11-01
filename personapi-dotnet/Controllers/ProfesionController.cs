using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;

namespace personapi_dotnet.Controllers
{
 public class ProfesionController : Controller
 {
 private readonly ProfesionRepository _repo;
 public ProfesionController(ProfesionRepository repo)
 {
 _repo = repo;
 }

 public async Task<IActionResult> Index()
 {
 return View(await _repo.findAll());
 }

 [HttpGet]
 public IActionResult Add()
 {
 return View("FormularioCrear");
 }

 [HttpPost]
 [ValidateAntiForgeryToken]
 public async Task<IActionResult> Add(Profesion profesion)
 {
 if (!ModelState.IsValid)
 return View("FormularioCrear", profesion);

 await _repo.create(profesion);
 return RedirectToAction(nameof(Index));
 }

 public async Task<IActionResult> Edit(int id)
 {
 var item = await _repo.findById(id);
 return View("FormularioEditar", item);
 }

 [HttpPost]
 [ValidateAntiForgeryToken]
 public async Task<IActionResult> Edit(Profesion profesion)
 {
 if (!ModelState.IsValid)
 return View("FormularioEditar", profesion);

 await _repo.update(profesion);
 return RedirectToAction(nameof(Index));
 }

 public async Task<IActionResult> Delete(int id)
 {
 await _repo.delete(id);
 return RedirectToAction(nameof(Index));
 }
 }
}
