using Microsoft.AspNetCore.Mvc;

namespace RunGroopWebApp.Controllers;

public class ClubController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}