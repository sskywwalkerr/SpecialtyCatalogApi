using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpecialtyCatalog.Client.Models;
using SpecialtyCatalog.Client.Services;

namespace SpecialtyCatalog.Client.Controllers;

// Страницы для работы со специальностями. 
public class SpecialtiesController : Controller
{
    private readonly SpecialtyApiClient _api;
    private readonly DirectionApiClient _directionsApi;

    public SpecialtiesController(SpecialtyApiClient api, DirectionApiClient directionsApi)
    {
        _api = api;
        _directionsApi = directionsApi;
    }

    // Кладёт в ViewBag список направлений для выпадающего списка
    private async Task LoadDirectionsAsync(int? selectedId = null)
    {
        var directions = await _directionsApi.GetAllAsync();
        ViewBag.Directions = new SelectList(directions, "Id", "Name", selectedId);
    }

    // GET /Specialties?directionId=1&search=...  список с фильтрами
    public async Task<IActionResult> Index(int? directionId, string? search)
    {
        await LoadDirectionsAsync(directionId);
        ViewBag.Search = search;
        return View(await _api.GetAllAsync(directionId, search));
    }

    // GET /Specialties/Create  пустая форма
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await LoadDirectionsAsync();
        return View();
    }

    // POST /Specialties/Create  сохранение
    [HttpPost]
    public async Task<IActionResult> Create(SpecialtyDto specialty)
    {
        if (await _api.CreateAsync(specialty))
        {
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError("", "API не смог сохранить специальность");
        await LoadDirectionsAsync(specialty.DirectionId);
        return View(specialty);
    }

    // GET /Specialties/Edit/1  форма с текущими данными
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var specialty = await _api.GetByIdAsync(id);
        if (specialty == null)
        {
            return NotFound();
        }

        await LoadDirectionsAsync(specialty.DirectionId);
        return View(specialty);
    }

    // POST /Specialties/Edit  сохранение изменений
    [HttpPost]
    public async Task<IActionResult> Edit(SpecialtyDto specialty)
    {
        if (await _api.UpdateAsync(specialty))
        {
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError("", "API не смог сохранить изменения");
        await LoadDirectionsAsync(specialty.DirectionId);
        return View(specialty);
    }

    // GET /Specialties/Delete/1  страница подтверждения
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var specialty = await _api.GetByIdAsync(id);
        return specialty == null ? NotFound() : View(specialty);
    }

    // POST /Specialties/Delete  удаление
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _api.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}