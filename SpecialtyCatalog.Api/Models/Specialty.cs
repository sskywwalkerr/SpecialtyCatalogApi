namespace SpecialtyCatalog.Api.Models;

/// Специальность
public class Specialty
{
    public int Id { get; set; } // Первичный ключ
    public string Code { get; set; } = string.Empty; // Код специальности, например 09.02.07
    public string Name { get; set; } = string.Empty; // Название специальности
    
    public int DirectionId { get; set; } // Внешний ключ: номер направления, к которому относится специальность
    
    public Direction? Direction { get; set; } // Само направление
}
