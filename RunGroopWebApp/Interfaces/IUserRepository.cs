using RunGroopWebApp.Models;

namespace RunGroopWebApp.Interfaces;

public interface IUserRepository
{
    bool Add(AppUser user);
    bool Delete(AppUser user);
    bool Update(AppUser user);
    bool Save();
    Task<IEnumerable<AppUser>> GetAllUsers();
    Task<AppUser> GetUserById(string id);
}
