using System.ComponentModel.DataAnnotations;

namespace Accurri.Models;

/// <summary>
/// Request to add an item.
/// </summary>
public sealed class AddTodoRequest
{
    /// <summary>
    /// Item description.
    /// </summary>
    [Required]
    public string Description { get; init; } = null!;
}
