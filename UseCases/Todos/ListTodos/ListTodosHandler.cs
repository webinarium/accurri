using Accurri.Entities;
using Accurri.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Accurri.UseCases.Todos.ListTodos;

internal sealed class ListTodosHandler(
    ILogger<ListTodosHandler> logger,
    IUnitOfWork unitOfWork
) : IRequestHandler<ListTodosQuery, IEnumerable<ToDo>>
{
    public async Task<IEnumerable<ToDo>> Handle(ListTodosQuery query, CancellationToken token)
    {
        logger.LogInformation("Handle ListTodoQuery");

        var todos = await unitOfWork.From<ToDo>().OrderBy(todo => todo.Id).ToListAsync(token);

        logger.LogDebug("Found {count} todos", todos.Count);

        return todos;
    }
}
