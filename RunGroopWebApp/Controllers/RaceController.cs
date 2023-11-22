using RunGroopWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RunGroopWebApp.Controllers;

public class RaceController : Controller
{
    private readonly ApplicationDbContext _context;
    public RaceController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var races = _context.Races.ToList();
        return View(races);
    }

    public IActionResult Detail(int id)
    {
        var race = _context.Races.Include(x => x.Address).FirstOrDefault(club => club.Id == id);
        return View(race);
    }
}