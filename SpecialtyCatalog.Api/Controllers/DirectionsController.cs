using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpecialtyCatalog.Api.Data;
using SpecialtyCatalog.Api.Models;

namespace SpecialtyCatalog.Api.Controllers;

/// контроллер направлений подготовки
[ApiController]
[Route("api/[controller]")]
public class DirectionsController : ControllerBase
{
    private readonly AppDbContext _db;

    public DirectionsController(AppDbContext db)
    {
        _db = db;
    }

    // GET /api/directions
    // GET /api/directions?search=информ
    [HttpGet]
    public async Task<ActionResult<List<Direction>>> GetAll(string? search)
    {
        var directions = await _db.Directions.OrderBy(d => d.Name).ToListAsync();

        if (!string.IsNullOrWhiteSpace(search))
        {
            directions = directions
                .Where(d => d.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        return directions;
    }

    // GET /api/directions/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Direction>> GetById(int id)
    {
        var direction = await _db.Directions.FindAsync(id); // есть ли запись в оперативной памяти

        if (direction == null)
        {
            return NotFound();
        }

        return direction;
    }

    // POST /api/directions
    // Добавляет направление
    [HttpPost]
    public async Task<ActionResult<Direction>> Create(Direction direction)
    {
        _db.Directions.Add(direction);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = direction.Id }, direction);
    }

    // PUT /api/directions/1
    // Редактирует название направления
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Direction direction)
    {
        if (id != direction.Id)
        {
            return BadRequest("Id в адресе и в теле запроса не совпадают");
        }

        var existing = await _db.Directions.FindAsync(id);

        if (existing == null)
        {
            return NotFound();
        }

        existing.Name = direction.Name;
        await _db.SaveChangesAsync();

        return NoContent();
    }

    // DELETE /api/directions/1 ю)
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var direction = await _db.Directions.FindAsync(id);

        if (direction == null)
        {
            return NotFound();
        }

        _db.Directions.Remove(direction);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
