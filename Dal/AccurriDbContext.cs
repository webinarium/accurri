// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

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
