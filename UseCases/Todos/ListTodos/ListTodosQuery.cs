using Accurri.Entities;

using MediatR;

namespace Accurri.UseCases.Todos.ListTodos;

internal sealed record ListTodosQuery : IRequest<IEnumerable<ToDo>>;
