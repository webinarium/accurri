// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Accurri.Models;

/// <summary>
/// Request to update an item.
/// </summary>
public sealed class UpdateTodoRequest
{
    /// <summary>
    /// Item description.
    /// </summary>
    [Required]
    public string Description { get; init; } = null!;

    /// <summary>
    /// Whether the item is complete.
    /// </summary>
    [DefaultValue(false)]
    public bool Complete { get; init; } = false;
}
