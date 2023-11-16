using Accurri.Dal;

using FluentMigrator.Runner;

using Microsoft.EntityFrameworkCore;

namespace Accurri.Extensions;

internal static class ServiceCollectionExtension
{
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
