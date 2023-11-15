// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System.Reflection;

using Accurri.Dal;
using Accurri.Services;

using FluentMigrator.Runner;

using Microsoft.EntityFrameworkCore;

const string apiTitle = "Accurri API";
const string apiVersion = "v1";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(apiVersion, new()
    {
        Title = apiTitle,
        Version = apiVersion
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

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

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseStaticFiles();
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/api/{documentName}/accurri.json";
    });

    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = apiTitle;
        options.RoutePrefix = "api";
        options.SwaggerEndpoint($"{apiVersion}/accurri.json", apiVersion);
    });
}

app.MapControllers();

// Migrate the database.
using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

app.Run();
