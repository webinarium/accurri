// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Microsoft.AspNetCore.Mvc;

namespace Accurri.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("/")]
public sealed class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [Route("")]
    public IActionResult Index()
    {
        _logger.LogInformation("Homepage");

        return View();
    }

    [Route("add")]
    public IActionResult Add()
    {
        _logger.LogInformation("Add");

        return View();
    }

    [Route("edit/{id:int}")]
    public IActionResult Edit(int id)
    {
        _logger.LogInformation("Edit {id}", id);

        return View(id);
    }
}
