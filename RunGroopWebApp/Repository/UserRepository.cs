using RunGroopWebApp.Data;
using RunGroopWebApp.Models;
using RunGroopWebApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace RunGroopWebApp.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public bool Add(AppUser user)
    {
        throw new NotImplementedException();
    }

    public bool Delete(AppUser user)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<AppUser>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<AppUser> GetUserById(string id)
    {
        return await _context.Users.FindAsync(id);
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();

        if (saved > 0)
        {
            return true;
        }

        return false;
    }

    public bool Update(AppUser user)
    {
        _context.Update(user);

        return Save();
    }
}
