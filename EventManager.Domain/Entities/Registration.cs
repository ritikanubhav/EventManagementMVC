using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EventManager.Domain.Entities
{
	public class Registration
	{
		public int Id { get; set; }
		public int EventId { get; set; }
		public string UserId { get; set; }
		public Event? Event { get; set; } 
		public ApplicationUser? User { get; set; } 
	}
}
