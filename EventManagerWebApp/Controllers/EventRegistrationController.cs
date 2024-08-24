using System.Security.Claims;
using EventManager.Domain.Entities;
using EventManager.Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace EventManagerWebApp.Controllers
{
    [Authorize]
    public class EventRegistrationController : Controller
    {
        private IEventRepo eventRepo;
        private IRegistrationRepo regRepo;
        private UserManager<ApplicationUser> userManager;
        // have similar RoleManager and others
        
        public EventRegistrationController(IEventRepo eventRepo,IRegistrationRepo regRepo, UserManager<ApplicationUser> userManager)
        {
            this.eventRepo = eventRepo;
            this.regRepo=regRepo;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var AllEvents = eventRepo.GetAllEvents();
            var EventsToRegister = (from e in AllEvents
                                    where e.DateAndTime > DateTime.Now
                                    select e).ToList();
            return View(EventsToRegister);
        }
        [HttpGet]
        public ActionResult Register(int eventId)
        {
            var e=eventRepo.GetEvent(eventId);
            bool alreadyRegistered=e.Registrations.ToList().Any(r => r.UserId == userManager.GetUserId(User) && r.EventId == eventId);
            bool isEventFull =e.Registrations.ToList().Count()>=e.MaxAttendees;

            if (isEventFull)
            {
                TempData["message"] = $"Registration for {e.Name} is Closed. Seats are Full.";
            }
            else if(alreadyRegistered)
            {
                TempData["message"] = $"Registration for {User.Identity.Name} already Done";
            }
            else {
                Registration registration = new Registration
                {
                    UserId = userManager.GetUserId(User),
                    EventId = eventId
                };
                regRepo.Create(registration);
                TempData["message"] = $"Registration for event: {e.Name} successful for {User.Identity.Name} ";
            }
            return RedirectToAction("Index");
        }

        //[Authorize(Roles ="User")]
        public ActionResult MyRegistrations()
        {
            var events = regRepo.GetEventsForUser(userManager.GetUserId(User));
            return View(events);
        }
        [HttpGet]
        public ActionResult Cancel(int id)
        {
            TempData["message"] = $"Registration cancelled for event.";
            regRepo.Cancel(id,userManager.GetUserId(User));
            return RedirectToAction("MyRegistrations");
        }
    }
}
