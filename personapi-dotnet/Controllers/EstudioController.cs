using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interface;

namespace personapi_dotnet.Controllers
{
 public class EstudioController : Controller
 {
 private readonly EstudioRepository _repo;
 private readonly IPersonaRepository _personaRepo;
 private readonly EntityInterface<Profesion> _profRepo;

 public EstudioController(EstudioRepository repo, IPersonaRepository personaRepo, EntityInterface<Profesion> profRepo)
 {
 _repo = repo;
 _personaRepo = personaRepo;
 _profRepo = profRepo;
 }

 public async Task<IActionResult> Index()
 {
 return View(await _repo.findAll());
 }

 [HttpGet]
 public async Task<IActionResult> Add()
 {
 ViewBag.Personas = await _personaRepo.findAll();
 ViewBag.Profesiones = await _profRepo.findAll();
 return View("FormularioCrear");
 }

 [HttpPost]
 [ValidateAntiForgeryToken]
 public async Task<IActionResult> Add(IFormCollection form)
 {
 // Parse form values manually to avoid model binding issues with DateOnly
 var estudio = new Estudio();

 if (int.TryParse(form["IdProf"], out var idProf))
 estudio.IdProf = idProf;

 if (int.TryParse(form["CcPer"], out var ccPer))
 estudio.CcPer = ccPer;

 estudio.Univer = form["Univer"];

 var fechaStr = form["Fecha"].ToString();
 if (!string.IsNullOrWhiteSpace(fechaStr))
 {
 if (DateOnly.TryParse(fechaStr, out var dateOnly))
 {
 estudio.Fecha = dateOnly;
 }
 else if (DateTime.TryParse(fechaStr, out var dt))
 {
 estudio.Fecha = DateOnly.FromDateTime(dt);
 }
 }

 // validate foreign keys exist
 var personas = await _personaRepo.findAll();
 var profesiones = await _profRepo.findAll();

 if (!personas.Any(p => p.Cc == estudio.CcPer))
 {
 ModelState.AddModelError(string.Empty, "La persona seleccionada no existe. Por favor registra primero la persona.");
 }

 if (!profesiones.Any(p => p.Id == estudio.IdProf))
 {
 ModelState.AddModelError(string.Empty, "La profesion seleccionada no existe. Por favor registra primero la profesion.");
 }

 if (!ModelState.IsValid)
 {
 ViewBag.Personas = personas;
 ViewBag.Profesiones = profesiones;
 return View("FormularioCrear", estudio);
 }

 try
 {
 await _repo.create(estudio);
 }
 catch (DbUpdateException ex)
 {
 ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
 ViewBag.Personas = personas;
 ViewBag.Profesiones = profesiones;
 return View("FormularioCrear", estudio);
 }

 return RedirectToAction(nameof(Index));
 }

 public async Task<IActionResult> Edit(int id)
 {
 var item = await _repo.findById(id);
 ViewBag.Personas = await _personaRepo.findAll();
 ViewBag.Profesiones = await _profRepo.findAll();
 return View("FormularioEditar", item);
 }

 [HttpPost]
 [ValidateAntiForgeryToken]
 public async Task<IActionResult> Edit(Estudio estudio)
 {
 if (!ModelState.IsValid)
 return View("FormularioEditar", estudio);

 await _repo.update(estudio);
 return RedirectToAction(nameof(Index));
 }

 public async Task<IActionResult> Delete(int id)
 {
 await _repo.delete(id);
 return RedirectToAction(nameof(Index));
 }
 }
}
