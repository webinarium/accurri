// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Accurri.Entities;
using Accurri.Exceptions;
using Accurri.Services;

using MediatR;

namespace Accurri.UseCases.Todos.UpdateTodo;

internal sealed class UpdateTodoHandler : IRequestHandler<UpdateTodoCommand>
{
    private readonly ILogger<UpdateTodoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTodoHandler(ILogger<UpdateTodoHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateTodoCommand command, CancellationToken token)
    {
        _logger.LogInformation("Handle UpdateTodoCommand");

        ToDo? todo = await _unitOfWork.FindAsync<int, ToDo>(command.Id, token);

        if (todo == null)
        {
            _logger.LogError("Todo with ID={id} not found", command.Id);
            throw new TodoNotFoundException();
        }

        todo.Description = command.Description;
        todo.Complete = command.Complete;

        _unitOfWork.Update(todo);
        await _unitOfWork.SaveAsync(token);

        _logger.LogDebug("Updated todo with ID={id}", todo.Id);
    }
}
