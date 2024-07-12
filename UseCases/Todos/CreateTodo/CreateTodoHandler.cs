using Accurri.Entities;
using Accurri.Services;

using MediatR;

namespace Accurri.UseCases.Todos.CreateTodo;

internal sealed class CreateTodoHandler : IRequestHandler<CreateTodoCommand, ToDo>
{
    private readonly ILogger<CreateTodoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTodoHandler(ILogger<CreateTodoHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<ToDo> Handle(CreateTodoCommand command, CancellationToken token)
    {
        _logger.LogInformation("Handle CreateTodoCommand");

        ToDo todo = new()
        {
            Description = command.Description,
            Complete = false
        };

        _unitOfWork.Add(todo);
        await _unitOfWork.SaveAsync(token);

        _logger.LogDebug("Created new todo with ID={id}", todo.Id);

        return todo;
    }
}
