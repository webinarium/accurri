using Accurri.Entities;
using Accurri.Exceptions;
using Accurri.Services;

using MediatR;

namespace Accurri.UseCases.Todos.GetTodo;

internal sealed class GetTodoHandler : IRequestHandler<GetTodoQuery, ToDo>
{
    private readonly ILogger<GetTodoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetTodoHandler(ILogger<GetTodoHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<ToDo> Handle(GetTodoQuery query, CancellationToken token)
    {
        _logger.LogInformation("Handle GetTodoQuery");

        ToDo? todo = await _unitOfWork.FindAsync<int, ToDo>(query.Id, token);

        if (todo == null)
        {
            _logger.LogError("Todo with ID={id} not found", query.Id);
            throw new TodoNotFoundException();
        }

        _logger.LogDebug("Retrieved todo with ID={id}", todo.Id);

        return todo;
    }
}
