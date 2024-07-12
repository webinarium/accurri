using Accurri.Entities;

using MediatR;

namespace Accurri.UseCases.Todos.GetTodo;

internal sealed record GetTodoQuery(int Id) : IRequest<ToDo>;
