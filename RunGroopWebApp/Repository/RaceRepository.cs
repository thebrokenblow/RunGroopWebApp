using RunGroopWebApp.Data;
using RunGroopWebApp.Models;
using RunGroopWebApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace RunGroopWebApp.Repository;

public class RaceRepository : IRaceRepository
{
    private readonly ApplicationDbContext _context;
    public RaceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Race>> GetAll()
    {
        return await _context.Races.ToListAsync();
    }

    public bool Add(Race race)
    {
        _context.Add(race);
        return Save();
    }

    public bool Delete(Race race)
    {
        _context.Remove(race);
        return Save();
    }

    public bool Update(Race race)
    {
        _context.Update(race);
        return Save();
    }

    public async Task<Race> GetByIdAsync(int id)
    {
        return await _context.Races.Include(race => race.Address).FirstOrDefaultAsync(race => race.Id == id);
    }

    public async Task<Race> GetByIdAsyncNoTracking(int id)
    {
        return await _context.Races.Include(x => x.Address).AsNoTracking().FirstOrDefaultAsync(race => race.Id == id);
    }

    public async Task<IEnumerable<Race>> GetRaceByCity(string city)
    {
        return await _context.Races.Include(race => race.Address).Where(race => race.Address.City == city).ToListAsync();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();

        if (saved > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}