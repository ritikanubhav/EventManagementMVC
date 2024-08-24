using EventManager.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Data
{
	public class EventManagerDbContext:IdentityDbContext<ApplicationUser>
	{
		//1. map to db
		public EventManagerDbContext() { }
		public EventManagerDbContext(DbContextOptions<EventManagerDbContext> options)
			: base(options)
		{
			//configuartion through mvc /web api -(connection string in config file on client side)
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//if configured already dont configure
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=EventManagerDB;Integrated Security=True;Encrypt=True");
			}
		}

		// You can add additional DbSets  other than User as it is made by identity class
		public DbSet<Registration> Registrations { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<ApplicationUser> ApplicationUsers{ get; set; } //optional
	}
}
