// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using FluentMigrator.Runner.VersionTableInfo;

namespace Accurri.Dal;

public sealed class VersionTableMetaData : IVersionTableMetaData
{
    public required object ApplicationContext { get; set; }

    public string SchemaName => "accurri";
    public string TableName => "VersionInfo";
    public string ColumnName => "Version";
    public string UniqueIndexName => "UC_Version";
    public string AppliedOnColumnName => "AppliedOn";
    public string DescriptionColumnName => "Description";
    public bool OwnsSchema => true;
}
