using RunGroopWebApp.Data;
using RunGroopWebApp.Models;
using RunGroopWebApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace RunGroopWebApp.Repository;

public class DashboardRepository : IDashboardRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<Club>> GetAllUserClub()
    {
        var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();

        if (currentUser != null)
        {
            var userClubs = await _context.Clubs.Where(x => x.AppUserId == currentUser).ToListAsync();

            return userClubs;
        }

        throw new NullReferenceException($"{ currentUser } is null");
    }

    public async Task<List<Race>> GetAllUserRaces()
    {
        var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();

        if (currentUser != null)
        {
            var userRaces = await _context.Races.Where(x => x.AppUserId == currentUser).ToListAsync();

            return userRaces;
        }

        throw new NullReferenceException($"{currentUser} is null");
    }
}