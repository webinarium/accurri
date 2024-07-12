using Accurri.Entities;
using Accurri.Services;
using MediatR;

namespace Accurri.UseCases.Todos.CreateTodo;

internal sealed class CreateTodoHandler(
    ILogger<CreateTodoHandler> logger,
    IUnitOfWork unitOfWork
) : IRequestHandler<CreateTodoCommand, ToDo>
{
    public async Task<ToDo> Handle(CreateTodoCommand command, CancellationToken token)
    {
        logger.LogInformation("Handle CreateTodoCommand");

        ToDo todo = new()
        {
            Description = command.Description,
            Complete = false
        };

        unitOfWork.Add(todo);
        await unitOfWork.SaveAsync(token);

        logger.LogDebug("Created new todo with ID={id}", todo.Id);

        return todo;
    }
}
