using Accurri.Entities;
using Accurri.Exceptions;
using Accurri.Services;

using MediatR;

namespace Accurri.UseCases.Todos.DeleteTodo;

internal sealed class DeleteTodoHandler : IRequestHandler<DeleteTodoCommand>
{
    private readonly ILogger<DeleteTodoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTodoHandler(ILogger<DeleteTodoHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteTodoCommand command, CancellationToken token)
    {
        _logger.LogInformation("Handle DeleteTodoCommand");

        ToDo? todo = await _unitOfWork.FindAsync<int, ToDo>(command.Id, token);

        if (todo == null)
        {
            _logger.LogError("Todo with ID={id} not found", command.Id);
            throw new TodoNotFoundException();
        }

        _unitOfWork.Remove(todo);
        await _unitOfWork.SaveAsync(token);

        _logger.LogDebug("Todo with ID={id} deleted", todo.Id);
    }
}
