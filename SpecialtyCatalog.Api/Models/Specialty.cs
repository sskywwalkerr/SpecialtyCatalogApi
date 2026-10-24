namespace SpecialtyCatalog.Api.Models;

/// Специальность
public class Specialty
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    
    public int DirectionId { get; set; } // Внешний ключ: номер направления, к которому относится специальность
    
    public Direction? Direction { get; set; } // Само направление
}
