using RunGroopWebApp.Data;
using RunGroopWebApp.Models;
using RunGroopWebApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace RunGroopWebApp.Repository;

public class ClubRepository : IClubRepository
{
    private readonly ApplicationDbContext _context;
    public ClubRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Club>> GetAll()
    {
        return await _context.Clubs.ToListAsync();
    }

    public bool Add(Club club)
    {
        _context.Add(club);
        return Save();
    }

    public bool Delete(Club club)
    {
        _context.Remove(club);
        return Save();
    }

    public async Task<Club> GetByIdAsync(int id)
    {
        return await _context.Clubs.Include(x => x.Address).FirstOrDefaultAsync(club => club.Id == id);
    }

    public async Task<IEnumerable<Club>> GetClubByCity(string city)
    {
        return await _context.Clubs.Include(x => x.Address).Where(club => club.Address.City == city).ToListAsync();
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

    public bool Update(Club club)
    {
        _context.Update(club);
        return Save();
    }
}