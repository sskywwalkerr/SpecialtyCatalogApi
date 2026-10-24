using Microsoft.AspNetCore.Mvc;
using SpecialtyCatalog.Client.Models;
using SpecialtyCatalog.Client.Services;

namespace SpecialtyCatalog.Client.Controllers;

// Страницы для работы с направлениями. Данные берёт из API через DirectionApiClient
public class DirectionsController : Controller
{
    private readonly DirectionApiClient _api;

    public DirectionsController(DirectionApiClient api)
    {
        _api = api;
    }

    // GET /Directions?search=...  список с фильтром по названию
    public async Task<IActionResult> Index(string? search)
    {
        ViewBag.Search = search;
        return View(await _api.GetAllAsync(search));
    }

    // GET /Directions/Create  пустая форма
    [HttpGet]
    public IActionResult Create() => View();

    // POST /Directions/Create  сохранение
    [HttpPost]
    public async Task<IActionResult> Create(DirectionDto direction)
    {
        if (await _api.CreateAsync(direction))
        {
            return RedirectToAction(nameof(Index)); // перенаправить пользователя на метод Index текущего контроллера
        }

        ModelState.AddModelError("", "API не смог сохранить направление");
        return View(direction);
    }

    // GET /Directions/Edit/1  форма с текущими данными
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var direction = await _api.GetByIdAsync(id);
        return direction == null ? NotFound() : View(direction);
    }

    // POST /Directions/Edit  сохранение изменений
    [HttpPost]
    public async Task<IActionResult> Edit(DirectionDto direction)
    {
        if (await _api.UpdateAsync(direction))
        {
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError("", "API не смог сохранить изменения");
        return View(direction);
    }

    // GET /Directions/Delete/1  страница подтверждения
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var direction = await _api.GetByIdAsync(id);
        return direction == null ? NotFound() : View(direction);
    }

    // POST /Directions/Delete  удаление
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _api.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
