using FluentMigrator.Runner.VersionTableInfo;

namespace Accurri.Dal;

/// <inheritdoc />
public sealed class VersionTableMetaData : IVersionTableMetaData
{
    /// <inheritdoc />
    public required object ApplicationContext { get; set; }

    /// <inheritdoc />
    public string SchemaName => "accurri";

    /// <inheritdoc />
    public string TableName => "VersionInfo";

    /// <inheritdoc />
    public string ColumnName => "Version";

    /// <inheritdoc />
    public string UniqueIndexName => "UC_Version";

    /// <inheritdoc />
    public string AppliedOnColumnName => "AppliedOn";

    /// <inheritdoc />
    public string DescriptionColumnName => "Description";

    /// <inheritdoc />
    public bool OwnsSchema => true;
}
