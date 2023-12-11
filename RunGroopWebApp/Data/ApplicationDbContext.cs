using RunGroopWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RunGroopWebApp.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser> 
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{

	}

	public DbSet<Race> Races { get; set; }
	public DbSet<Club> Clubs { get; set; }
	public DbSet<Address> Addresses { get; set; }
}
