using FluentMigrator;
using FluentMigrator.Expressions;
using FluentMigrator.Infrastructure;

namespace Accurri.Dal;

/// <summary>
/// Base class for database migrations.
/// </summary>
public abstract class AbstractMigration : IMigration
{
    /// <summary>
    /// Returns SQL to migrate the database up.
    /// </summary>
    protected abstract string GetUpSql(IServiceProvider services);

    /// <summary>
    /// Returns SQL to migrate the database down.
    /// </summary>
    protected abstract string GetDownSql(IServiceProvider services);

    /// <inheritdoc />
    public object ApplicationContext => null!;

    /// <inheritdoc />
    public string ConnectionString => null!;

    /// <inheritdoc />
    public void GetUpExpressions(IMigrationContext context)
    {
        if (!context.QuerySchema.SchemaExists("accurri"))
        {
            context.Expressions.Add(new ExecuteSqlStatementExpression
            {
                SqlStatement = "create schema accurri;"
            });
        }

        context.Expressions.Add(new ExecuteSqlStatementExpression
        {
            SqlStatement = "set search_path to accurri;"
        });

        context.Expressions.Add(new ExecuteSqlStatementExpression
        {
            SqlStatement = GetUpSql(context.ServiceProvider)
        });
    }

    /// <inheritdoc />
    public void GetDownExpressions(IMigrationContext context)
    {
        context.Expressions.Add(new ExecuteSqlStatementExpression
        {
            SqlStatement = "set search_path to accurri;"
        });

        context.Expressions.Add(new ExecuteSqlStatementExpression
        {
            SqlStatement = GetDownSql(context.ServiceProvider)
        });
    }
}
