// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System.ComponentModel.DataAnnotations;

namespace Accurri.Models;

public sealed record UpdateModel(
    [Required]
    string Description,

    [Required]
    bool Complete
);
