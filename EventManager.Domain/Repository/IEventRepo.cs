using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManager.Domain.Entities;

namespace EventManager.Domain.Repository
{
	public interface IEventRepo
	{
		public void Create(Event e);
		public void Update(Event e);
		public Event GetEvent(int id);
		public ICollection<Event> GetAllEvents();	
		public void Delete(int id );

	}
}
