// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using MediatR;

namespace Accurri.UseCases.Todos.UpdateTodo;

internal sealed record UpdateTodoCommand(int Id, string Description, bool Complete) : IRequest;
