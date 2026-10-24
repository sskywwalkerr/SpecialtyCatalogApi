using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpecialtyCatalog.Api.Data;
using SpecialtyCatalog.Api.Models;

namespace SpecialtyCatalog.Api.Controllers;

/// контроллер специальностей.
[ApiController]
[Route("api/[controller]")]
public class SpecialtiesController : ControllerBase
{
    private readonly AppDbContext _db;

    public SpecialtiesController(AppDbContext db)
    {
        _db = db;
    }

    // GET /api/specialties
    // GET /api/specialties?directionId=1
    // GET /api/specialties?search=программ
    [HttpGet]
    public async Task<ActionResult<List<Specialty>>> GetAll(int? directionId, string? search)
    {
        var query = _db.Specialties.Include(s => s.Direction).AsQueryable();

        if (directionId != null)
        {
            query = query.Where(s => s.DirectionId == directionId);
        }

        var specialties = await query.OrderBy(s => s.Code).ToListAsync();

        return specialties
            .Where(s => search == null
                        || s.Name.Contains(search, StringComparison.OrdinalIgnoreCase) // нечувствительный к регистрам
                        || s.Code.Contains(search))
            .ToList();
    }

    // GET /api/specialties/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Specialty>> GetById(int id)
    {
        var specialty = await _db.Specialties
            .Include(s => s.Direction)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (specialty == null)
        {
            return NotFound();
        }

        return specialty;
    }

    // POST /api/specialties
    // Добавляет специальность. Направление с указанным DirectionId должно существовать.
    [HttpPost]
    public async Task<ActionResult<Specialty>> Create(Specialty specialty)
    {
        if (!await _db.Directions.AnyAsync(d => d.Id == specialty.DirectionId))
        {
            return BadRequest("Направление с таким Id не найдено");
        }

        _db.Specialties.Add(specialty);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = specialty.Id }, specialty);
    }

    // PUT /api/specialties/1
    // Редактирует код, название и направление специальности
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Specialty specialty)
    {
        if (id != specialty.Id)
        {
            return BadRequest("Id в адресе и в теле запроса не совпадают");
        }

        var existing = await _db.Specialties.FindAsync(id);

        if (existing == null)
        {
            return NotFound();
        }

        if (!await _db.Directions.AnyAsync(d => d.Id == specialty.DirectionId))
        {
            return BadRequest("Направление с таким Id не найдено");
        }

        existing.Code = specialty.Code;
        existing.Name = specialty.Name;
        existing.DirectionId = specialty.DirectionId;
        await _db.SaveChangesAsync();

        return NoContent();
    }

    // DELETE /api/specialties/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var specialty = await _db.Specialties.FindAsync(id);

        if (specialty == null)
        {
            return NotFound();
        }

        _db.Specialties.Remove(specialty);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}