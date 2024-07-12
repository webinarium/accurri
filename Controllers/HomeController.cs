using Microsoft.AspNetCore.Mvc;

namespace Accurri.Controllers;

#pragma warning disable CS1591

[ApiExplorerSettings(IgnoreApi = true)]
[Route("/")]
public sealed class HomeController(ILogger<HomeController> logger) : Controller
{
    [Route("")]
    public IActionResult Index()
    {
        logger.LogInformation("Homepage");

        return View();
    }

    [Route("add")]
    public IActionResult Add()
    {
        logger.LogInformation("Add");

        return View();
    }

    [Route("edit/{id:int}")]
    public IActionResult Edit(int id)
    {
        logger.LogInformation("Edit {id}", id);

        return View(id);
    }
}
