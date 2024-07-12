using Accurri.Entities;
using Accurri.Exceptions;
using Accurri.Services;
using MediatR;

namespace Accurri.UseCases.Todos.UpdateTodo;

internal sealed class UpdateTodoHandler(
    ILogger<UpdateTodoHandler> logger,
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdateTodoCommand>
{
    public async Task Handle(UpdateTodoCommand command, CancellationToken token)
    {
        logger.LogInformation("Handle UpdateTodoCommand");

        ToDo? todo = await unitOfWork.FindAsync<int, ToDo>(command.Id, token);

        if (todo == null)
        {
            logger.LogError("Todo with ID={id} not found", command.Id);
            throw new TodoNotFoundException();
        }

        todo.Description = command.Description;
        todo.Complete = command.Complete;

        unitOfWork.Update(todo);
        await unitOfWork.SaveAsync(token);

        logger.LogDebug("Updated todo with ID={id}", todo.Id);
    }
}
