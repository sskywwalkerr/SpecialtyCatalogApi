namespace SpecialtyCatalog.Client.Models;

public class SpecialtyDto
{
    public int Id { get; set; }

    // Код специальности
    public string Code { get; set; } = string.Empty;

    // Название специальности
    public string Name { get; set; } = string.Empty;

    // Id направления, к которому относится специальность
    public int DirectionId { get; set; }

    // Направление целиком, API присылает его в ответах на GET
    public DirectionDto? Direction { get; set; }
}
