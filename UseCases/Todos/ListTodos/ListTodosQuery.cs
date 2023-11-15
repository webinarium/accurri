// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Accurri.Entities;

using MediatR;

namespace Accurri.UseCases.Todos.ListTodos;

internal sealed record ListTodosQuery : IRequest<IEnumerable<ToDo>>;
