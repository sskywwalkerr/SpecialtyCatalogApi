using System.Text.Json.Serialization;

namespace SpecialtyCatalog.Api.Models;

/// Направление подготовки
public class Direction
{
    public int Id { get; set; } // Первичный ключ
    public string Name { get; set; } = string.Empty; // Название направления
    
    [JsonIgnore] // нужен, чтобы в JSON не получился бесконечный цикл
    public List<Specialty> Specialties { get; set; } = new();
}
