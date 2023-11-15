// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

namespace Accurri.Entities;

/// <summary>
/// ToDo item.
/// </summary>
public sealed class ToDo
{
    /// <summary>
    /// Item ID.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Item description.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Whether the item is complete.
    /// </summary>
    public bool Complete { get; set; }
}
