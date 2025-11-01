using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interface;

namespace personapi_dotnet.Controllers
{
 public class TelefonoController : Controller
 {
 private readonly TelefonoRepository _repo;
 private readonly IPersonaRepository _personaRepo;

 public TelefonoController(TelefonoRepository repo, IPersonaRepository personaRepo)
 {
 _repo = repo;
 _personaRepo = personaRepo;
 }

 public async Task<IActionResult> Index()
 {
 return View(await _repo.findAll());
 }

 [HttpGet]
 public async Task<IActionResult> Add()
 {
 ViewBag.Personas = await _personaRepo.findAll();
 return View("FormularioCrear");
 }

 [HttpPost]
 [ValidateAntiForgeryToken]
 public async Task<IActionResult> Add(IFormCollection form)
 {
 var telefono = new Telefono();
 telefono.Num = form["Num"];
 telefono.Oper = form["Oper"];
 if (int.TryParse(form["Duenio"], out var duenio))
 telefono.Duenio = duenio;

 var personas = await _personaRepo.findAll();
 if (!personas.Any(p => p.Cc == telefono.Duenio))
 {
 ModelState.AddModelError(string.Empty, "La persona seleccionada no existe. Por favor registra primero la persona.");
 ViewBag.Personas = personas;
 return View("FormularioCrear", telefono);
 }

 try
 {
 await _repo.create(telefono);
 }
 catch (DbUpdateException ex)
 {
 ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
 ViewBag.Personas = personas;
 return View("FormularioCrear", telefono);
 }

 return RedirectToAction(nameof(Index));
 }

 public async Task<IActionResult> Edit(int id)
 {
 var item = await _repo.findById(id);
 ViewBag.Personas = await _personaRepo.findAll();
 return View("FormularioEditar", item);
 }

 [HttpPost]
 [ValidateAntiForgeryToken]
 public async Task<IActionResult> Edit(Telefono telefono)
 {
 if (!ModelState.IsValid)
 return View("FormularioEditar", telefono);

 await _repo.update(telefono);
 return RedirectToAction(nameof(Index));
 }

 public async Task<IActionResult> Delete(int id)
 {
 await _repo.delete(id);
 return RedirectToAction(nameof(Index));
 }
 }
}
