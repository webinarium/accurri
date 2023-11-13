// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

namespace Accurri.Entities;

public sealed class ToDo
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public bool Complete { get; set; }
}
