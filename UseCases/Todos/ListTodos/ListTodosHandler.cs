using Accurri.Entities;
using Accurri.Services;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Accurri.UseCases.Todos.ListTodos;

internal sealed class ListTodosHandler : IRequestHandler<ListTodosQuery, IEnumerable<ToDo>>
{
    private readonly ILogger<ListTodosHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ListTodosHandler(ILogger<ListTodosHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ToDo>> Handle(ListTodosQuery query, CancellationToken token)
    {
        _logger.LogInformation("Handle ListTodoQuery");

        var todos = await _unitOfWork.From<ToDo>().OrderBy(todo => todo.Id).ToListAsync(token);

        _logger.LogDebug("Found {count} todos", todos.Count);

        return todos;
    }
}
