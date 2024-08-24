using EventManager.Domain.Entities;
using EventManager.Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagerWebApp.Controllers
{
    public class EventController : Controller
    {
        private IEventRepo eventRepo;

        public EventController(IEventRepo eventRepo) 
        {
            this.eventRepo=eventRepo;
        }
        public IActionResult Index()
        {
            var events=eventRepo.GetAllEvents().ToList();
            return View(events);
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Event e)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            eventRepo.Create(e);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            eventRepo.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var e=eventRepo.GetEvent(id);
            return View(e);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Event e)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            eventRepo.Update(e);
            return RedirectToAction("Index");
        }
    }
}
