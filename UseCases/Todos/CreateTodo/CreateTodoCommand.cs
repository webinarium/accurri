using Accurri.Entities;
using MediatR;

namespace Accurri.UseCases.Todos.CreateTodo;

internal sealed record CreateTodoCommand(string Description) : IRequest<ToDo>;
