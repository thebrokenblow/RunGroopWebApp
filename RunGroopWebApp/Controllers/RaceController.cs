using Microsoft.AspNetCore.Mvc;

namespace RunGroopWebApp.Controllers;

public class RaceController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}