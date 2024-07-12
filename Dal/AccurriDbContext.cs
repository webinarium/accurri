using Accurri.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accurri.Dal;

internal sealed class AccurriDbContext(DbContextOptions<AccurriDbContext> options) : DbContext(options)
{
    public required DbSet<ToDo> ToDos { get; init; }
}
