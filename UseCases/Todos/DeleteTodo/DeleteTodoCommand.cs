using MediatR;

namespace Accurri.UseCases.Todos.DeleteTodo;

internal sealed record DeleteTodoCommand(int Id) : IRequest;
