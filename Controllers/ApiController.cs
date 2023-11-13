// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Accurri.Entities;
using Accurri.Models;
using Accurri.Services;

using Microsoft.AspNetCore.Mvc;

namespace Accurri.Controllers;

[ApiController]
[Route("/todo")]
public sealed class ApiController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ApiController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("")]
    public IActionResult List()
    {
        _logger.LogInformation("GET /todo");

        var todos = _unitOfWork.From<ToDo>().OrderBy(todo => todo.Id).ToList();

        _logger.LogDebug("Found {count} todos", todos.Count);

        return Ok(todos);
    }

    [HttpPost("")]
    public async Task<IActionResult> Add(AddModel model)
    {
        _logger.LogInformation("POST /todo");

        ToDo todo = new ToDo
        {
            Description = model.Description,
            Complete = false
        };

        _unitOfWork.Add(todo);
        await _unitOfWork.SaveAsync();

        _logger.LogDebug("Created new todo with ID={id}", todo.Id);

        return Created(string.Empty, todo.Id.ToString());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        _logger.LogInformation($"GET /todo/{id}");

        ToDo? todo = await _unitOfWork.FindAsync<int, ToDo>(id);

        if (todo == null)
        {
            _logger.LogError("Todo with ID={id} not found", id);

            return NotFound();
        }

        _logger.LogDebug("Retrieved todo with ID={id}", todo.Id);

        return Ok(todo);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateModel model)
    {
        _logger.LogInformation($"PUT /todo/{id}");

        ToDo? todo = await _unitOfWork.FindAsync<int, ToDo>(id);

        if (todo == null)
        {
            _logger.LogError("Todo with ID={id} not found", id);

            return NotFound();
        }

        todo.Description = model.Description;
        todo.Complete = model.Complete;

        _unitOfWork.Update(todo);
        await _unitOfWork.SaveAsync();

        _logger.LogDebug("Updated todo with ID={id}", todo.Id);

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remove(int id)
    {
        _logger.LogInformation($"DELETE /todo/{id}");

        ToDo? todo = await _unitOfWork.FindAsync<int, ToDo>(id);

        if (todo == null)
        {
            _logger.LogError("Todo with ID={id} not found", id);

            return Ok();
        }

        _unitOfWork.Remove(todo);
        await _unitOfWork.SaveAsync();

        _logger.LogDebug("Todo with ID={id} deleted", todo.Id);

        return Ok();
    }
}
