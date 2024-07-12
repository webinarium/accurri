using Accurri.Entities;

using Microsoft.EntityFrameworkCore;

namespace Accurri.Dal;

internal sealed class AccurriDbContext : DbContext
{
    public AccurriDbContext(DbContextOptions<AccurriDbContext> options) : base(options)
    {
    }

    public required DbSet<ToDo> ToDos { get; set; }
}
