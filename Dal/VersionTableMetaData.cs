// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

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
