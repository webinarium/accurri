// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Accurri.Dal;
using Accurri.Services;

using FluentMigrator.Runner;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(runnerBuilder => runnerBuilder
        .AddPostgres()
        .WithGlobalConnectionString(connectionString)
        .ScanIn(typeof(AbstractMigration).Assembly).For.Migrations()
        .ScanIn(typeof(VersionTableMetaData).Assembly).For.VersionTableMetaData()
    )
    .AddLogging(runnerBuilder => runnerBuilder.AddFluentMigratorConsole());

builder.Services.AddDbContext<AccurriDbContext>(options =>
{
    options.UseNpgsql($"{connectionString};Search Path=accurri");
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseStaticFiles();
app.UseRouting();

app.MapControllers();

// Migrate the database.
using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

app.Run();
