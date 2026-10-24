using Microsoft.EntityFrameworkCore;
using SpecialtyCatalog.Api.Models;

namespace SpecialtyCatalog.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Direction> Directions => Set<Direction>();
    public DbSet<Specialty> Specialties => Set<Specialty>();
}
