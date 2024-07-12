using Accurri.Entities;
using Accurri.Exceptions;
using Accurri.Services;
using MediatR;

namespace Accurri.UseCases.Todos.GetTodo;

internal sealed class GetTodoHandler(
    ILogger<GetTodoHandler> logger,
    IUnitOfWork unitOfWork
) : IRequestHandler<GetTodoQuery, ToDo>
{
    public async Task<ToDo> Handle(GetTodoQuery query, CancellationToken token)
    {
        logger.LogInformation("Handle GetTodoQuery");

        ToDo? todo = await unitOfWork.FindAsync<int, ToDo>(query.Id, token);

        if (todo == null)
        {
            logger.LogError("Todo with ID={id} not found", query.Id);
            throw new TodoNotFoundException();
        }

        logger.LogDebug("Retrieved todo with ID={id}", todo.Id);

        return todo;
    }
}
