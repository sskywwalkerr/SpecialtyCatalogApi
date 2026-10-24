using SpecialtyCatalog.Api.Models;

namespace SpecialtyCatalog.Api.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (db.Directions.Any())
        {
            return;
        }

        var directions = new List<Direction>
        {
            new()
            {
                Name = "Информационные технологии",
                Specialties =
                {
                    new Specialty { Code = "09.02.07", Name = "Информационные системы и программирование" },
                    new Specialty { Code = "09.02.01", Name = "Компьютерные системы и комплексы" },
                    new Specialty { Code = "10.02.05", Name = "Обеспечение информационной безопасности автоматизированных систем" }
                }
            },
            new()
            {
                Name = "Экономика и управление",
                Specialties =
                {
                    new Specialty { Code = "38.02.01", Name = "Экономика и бухгалтерский учет" },
                    new Specialty { Code = "38.02.04", Name = "Коммерция" }
                }
            },
            new()
            {
                Name = "Образование и педагогика",
                Specialties =
                {
                    new Specialty { Code = "44.02.01", Name = "Дошкольное образование" },
                    new Specialty { Code = "44.02.02", Name = "Преподавание в начальных классах" }
                }
            }
        };

        db.Directions.AddRange(directions);
        db.SaveChanges();
    }
}