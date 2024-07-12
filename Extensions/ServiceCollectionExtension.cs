using Accurri.Dal;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Accurri.Extensions;

internal static class ServiceCollectionExtension
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        const string apiTitle = "Accurri API";
        const string apiVersion = "v1";

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(apiVersion, new()
            {
                Title = apiTitle,
                Version = apiVersion
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddFluentMigratorCore()
            .ConfigureRunner(runnerBuilder => runnerBuilder
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(AbstractMigration).Assembly).For.Migrations()
                .ScanIn(typeof(VersionTableMetaData).Assembly).For.VersionTableMetaData()
            )
            .AddLogging(runnerBuilder => runnerBuilder.AddFluentMigratorConsole());

        services.AddDbContext<AccurriDbContext>(options =>
        {
            options.UseNpgsql($"{connectionString};Search Path=accurri");
        });

        return services;
    }
}
