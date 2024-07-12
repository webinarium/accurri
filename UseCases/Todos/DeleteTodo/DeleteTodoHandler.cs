using Accurri.Entities;
using Accurri.Exceptions;
using Accurri.Services;
using MediatR;

namespace Accurri.UseCases.Todos.DeleteTodo;

internal sealed class DeleteTodoHandler(
    ILogger<DeleteTodoHandler> logger,
    IUnitOfWork unitOfWork
) : IRequestHandler<DeleteTodoCommand>
{
    public async Task Handle(DeleteTodoCommand command, CancellationToken token)
    {
        logger.LogInformation("Handle DeleteTodoCommand");

        ToDo? todo = await unitOfWork.FindAsync<int, ToDo>(command.Id, token);

        if (todo == null)
        {
            logger.LogError("Todo with ID={id} not found", command.Id);
            throw new TodoNotFoundException();
        }

        unitOfWork.Remove(todo);
        await unitOfWork.SaveAsync(token);

        logger.LogDebug("Todo with ID={id} deleted", todo.Id);
    }
}
