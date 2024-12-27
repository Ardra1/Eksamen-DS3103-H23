namespace FORMULAONEAPI.Contexts;

using Microsoft.EntityFrameworkCore;
using FORMULAONEAPI.Models;

public class FormulaOneContext : DbContext
{
    public FormulaOneContext(DbContextOptions<FormulaOneContext> options):base(options){}

    public DbSet<Drivers>Drivers{get; set;}
    public DbSet<Teams>Teams{get; set;}
}

