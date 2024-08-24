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

    public class RegistrartionRepo : IRegistrationRepo
    {
        private EventManagerDbContext db = new EventManagerDbContext();

        public void Cancel(int eventid, string userId)
        {
            var registeration =( from r in db.Registrations
                                where r.UserId == userId && r.EventId == eventid
                                select r).FirstOrDefault();
            if (registeration != null)
            {
                db.Registrations.Remove(registeration);
                db.SaveChanges();
            }
        }

        public void Create(Registration registration)
        {
            db.Registrations.Add(registration);
            db.SaveChanges();
        }

        public List<Registration> GetAllRegistrations()
        {
            return db.Registrations.ToList();
        }

        public List<Event> GetEventsForUser(string userId)
        {
            var events=(from r in db.Registrations.Include(r => r.Event)
                       where r.UserId==userId select r.Event).ToList();
            return events;
        }

    }
}
