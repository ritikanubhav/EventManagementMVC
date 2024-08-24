using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManager.Domain.Entities;
using EventManager.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Data
{
	public class EventRepo : IEventRepo
	{
		private EventManagerDbContext db=new EventManagerDbContext();
		public void Create(Event e)
		{
			db.Events.Add(e);
			db.SaveChanges();
		}

		public void Delete(int id)
		{
			var e = db.Events.Find(id);
			db.Events.Remove(e);
			db.SaveChanges();
		}

		public ICollection<Event> GetAllEvents()
		{
			var events=db.Events.Include(e=>e.Registrations).ToList();
			return events;
		}

        public Event GetEvent(int id)
        {
            return db.Events
             .Include(e => e.Registrations).ThenInclude(r=>r.User)
             .FirstOrDefault(e => e.Id == id);
        }
        public void Update(Event e)
		{
			db.Update(e);
			db.SaveChanges();
		}
	}
}
