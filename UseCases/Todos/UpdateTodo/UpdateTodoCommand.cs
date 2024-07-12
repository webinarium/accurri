using MediatR;

namespace Accurri.UseCases.Todos.UpdateTodo;

internal sealed record UpdateTodoCommand(int Id, string Description, bool Complete) : IRequest;
