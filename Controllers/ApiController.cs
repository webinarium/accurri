using Accurri.Entities;
using Accurri.Exceptions;
using Accurri.Models;
using Accurri.UseCases.Todos.CreateTodo;
using Accurri.UseCases.Todos.DeleteTodo;
using Accurri.UseCases.Todos.GetTodo;
using Accurri.UseCases.Todos.ListTodos;
using Accurri.UseCases.Todos.UpdateTodo;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Accurri.Controllers;

/// <summary>
/// API for ToDo resources.
/// </summary>
[ApiController]
[Route("/todo")]
public sealed class ApiController : Controller
{
    private readonly ILogger<ApiController> _logger;
    private readonly IMediator _mediator;

    /// <summary>
    /// Dependency injection.
    /// </summary>
    public ApiController(ILogger<ApiController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Returns list of existing resources.
    /// </summary>
    [HttpGet("", Name = "todo_list")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ToDo>))]
    public async Task<IActionResult> List()
    {
        _logger.LogInformation("GET /todo");

        var query = new ListTodosQuery();
        var todos = await _mediator.Send(query);

        return Ok(todos);
    }

    /// <summary>
    /// Creates new resource.
    /// </summary>
    /// <response code="201">Returns the newly created resource.</response>
    [HttpPost("", Name = "todo_add")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ToDo))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(AddTodoRequest request)
    {
        _logger.LogInformation("POST /todo");

        var command = new CreateTodoCommand(
            Description: request.Description
        );

        var todo = await _mediator.Send(command);

        string? url = Url.Link("todo_get", new
        {
            id = todo.Id
        });

        return Created(url ?? string.Empty, todo);
    }

    /// <summary>
    /// Returns specified resource.
    /// </summary>
    [HttpGet("{id:int}", Name = "todo_get")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ToDo))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        _logger.LogInformation($"GET /todo/{id}");

        try
        {
            var query = new GetTodoQuery(
                Id: id
            );

            var todo = await _mediator.Send(query);

            return Ok(todo);
        }
        catch (TodoNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Updates specified resource.
    /// </summary>
    [HttpPut("{id:int}", Name = "todo_update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateTodoRequest request)
    {
        _logger.LogInformation($"PUT /todo/{id}");

        try
        {
            var command = new UpdateTodoCommand(
                Id: id,
                Description: request.Description,
                Complete: request.Complete
            );

            await _mediator.Send(command);

            return Ok();
        }
        catch (TodoNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes specified resource.
    /// </summary>
    [HttpDelete("{id:int}", Name = "todo_remove")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Remove(int id)
    {
        _logger.LogInformation($"DELETE /todo/{id}");

        try
        {
            var command = new DeleteTodoCommand(
                Id: id
            );

            await _mediator.Send(command);

            return Ok();
        }
        catch (TodoNotFoundException)
        {
            return Ok();
        }
    }
}
