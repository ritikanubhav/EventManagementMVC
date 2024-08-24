using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Domain.Entities
{
	public class Event
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage ="Please enter name:")]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]

		public DateTime DateAndTime { get; set; }
		[Required]
		public string Location { get; set; }
		[Required]
		public int MaxAttendees { get; set; }
		[Required]
		public decimal? Price { get; set; }
		public ICollection<Registration>? Registrations { get; set; }//= new List<Registration>();
	}
}
