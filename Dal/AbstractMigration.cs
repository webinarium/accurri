// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using FluentMigrator;
using FluentMigrator.Expressions;
using FluentMigrator.Infrastructure;

namespace Accurri.Dal;

public abstract class AbstractMigration : IMigration
{
    protected abstract string GetUpSql(IServiceProvider services);
    protected abstract string GetDownSql(IServiceProvider services);

    public object ApplicationContext => null!;
    public string ConnectionString => null!;

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
