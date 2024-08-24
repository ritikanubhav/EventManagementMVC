using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManager.Domain.Entities;

namespace EventManager.Domain.Repository
{
	public  interface IRegistrationRepo
	{
		public List<Registration> GetAllRegistrations();
		public void Create(Registration registration);
		public List<Event> GetEventsForUser(string userId);
		public void Cancel(int eventid, string userId);
	}
}
