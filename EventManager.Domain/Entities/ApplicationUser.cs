﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EventManager.Domain.Entities
{
	public class ApplicationUser:IdentityUser
	{
		public ICollection<Registration> Registrations { get; set; } =new List<Registration>();
	}
}
