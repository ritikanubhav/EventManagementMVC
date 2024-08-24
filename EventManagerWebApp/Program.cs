using EventManager.Data;
using EventManager.Domain.Entities;
using EventManager.Domain.Repository;
using EventManagerWebApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventManagerWebApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			

			//1. removed application db context AND added our own dbcontext
			builder.Services.AddDbContext<EventManagerDbContext>(options =>
				options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //2.Adding applicationUser(inherits from Identity user) in place of default IdentityUser and giving our own db context also
            builder.Services.AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<EventManagerDbContext>();

            //3.Identity Pages: If you use Identity for authentication and authorization, it often relies on Razor Pages for its default login, register, and other related views. Adding AddRazorPages() ensures these pages are correctly set up and accessible.

            builder.Services.AddRazorPages(); //-----------added extra

			builder.Services.AddControllersWithViews();

			//4. AddTransient for repo objects
			builder.Services.AddTransient<IEventRepo, EventRepo>();
			builder.Services.AddTransient<IRegistrationRepo, RegistrartionRepo>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");
			app.MapRazorPages();

			app.Run();
		}
	}
}
