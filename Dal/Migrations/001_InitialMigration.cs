using Accurri.Dal;
using FluentMigrator;
using FluentMigrator.Expressions;
using FluentMigrator.Infrastructure;

namespace Accurri.Dal.Migrations;

[Migration(1, "Initial migration")]
public sealed class InitialMigration : AbstractMigration
{
    protected override string GetUpSql(IServiceProvider services)
    {
        return """
            create table "ToDos" (
                "Id" serial primary key,
                "Description" text not null,
                "Complete" boolean not null default false
            );

            insert into "ToDos" ("Description", "Complete") values ('Application Startup', true);
            insert into "ToDos" ("Description", "Complete") values ('Kestrel & HTTP Context', true);
            insert into "ToDos" ("Description", "Complete") values ('Dependency Injection', true);
            insert into "ToDos" ("Description", "Complete") values ('Middlewares & Filters', false);
            insert into "ToDos" ("Description", "Complete") values ('Configuration & Deployment', false);
            insert into "ToDos" ("Description", "Complete") values ('Routing & Controllers', false);
            insert into "ToDos" ("Description", "Complete") values ('Model Binding & Validation', false);
            insert into "ToDos" ("Description", "Complete") values ('Multi-threading', false);
            insert into "ToDos" ("Description", "Complete") values ('Entity Framework Core', true);
            insert into "ToDos" ("Description", "Complete") values ('Background Tasks', false);
            insert into "ToDos" ("Description", "Complete") values ('Authentication & Authorization', false);
            insert into "ToDos" ("Description", "Complete") values ('Logging & Tracing', false);
            insert into "ToDos" ("Description", "Complete") values ('Unit Testing & Code Coverage', false);
            insert into "ToDos" ("Description", "Complete") values ('Code Style & Static Analysis', false);
        """;
    }

    protected override string GetDownSql(IServiceProvider services)
    {
        return """
            drop table if exists "ToDos";
        """;
    }
}
