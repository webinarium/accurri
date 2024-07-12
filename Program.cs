using Accurri.Extensions;
using Accurri.Services;

using FluentMigrator.Runner;

const string apiTitle = "Accurri API";
const string apiVersion = "v1";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseStaticFiles();
app.UseRouting();
app.UseStopwatchMiddleware();
app.UseTransactionMiddleware();

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
