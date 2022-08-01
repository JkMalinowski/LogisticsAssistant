using LogisticsAssistant.Models;
using LogisticsAssistant.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LogisticsAssistant.Controllers
{
    [Authorize]
    public class ScheduledTripsController : Controller
    {
        private readonly IScheduledTripRepository _context;

        public ScheduledTripsController(IScheduledTripRepository context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return View(_context.GetAll().Where(x => x.TripDescription.Contains(searchString)));
            }
            return View(_context.GetAll());
        }

        public IActionResult Details(int id)
        {
            return View(_context.Get(id));
        }

        public IActionResult Create()
        {
            ViewData["LorryId"] = new SelectList(_context.GetAllLorries(), "Id", "LorryBrand");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("LorryId,TripDescription,Distance,DateOfDepartue")] ScheduledTrips scheduledTrip)
        {
            scheduledTrip.CreationTripDate = DateTime.Now;
            var lorry = _context.GetAllLorries().FirstOrDefaultAsync(x => x.Id == scheduledTrip.LorryId);
            scheduledTrip.DateOfArrival = CalcDate.CalcArrivalDate(scheduledTrip.DateOfDepartue, scheduledTrip.Distance, lorry.Result.MaxSpeed, lorry.Result.BreakInMinutes, lorry.Result.BreakAfterRideInHours);
            var lorryTrips = _context.GetAll().Where(x => x.LorryId == scheduledTrip.LorryId);
            if (!TripDateValidator.IsTripDateValid(lorryTrips, scheduledTrip.DateOfDepartue, scheduledTrip.DateOfArrival))
            {
                ModelState.AddModelError("DatesOverlap", "Trip overlap with another trip!");
            }
            if (ModelState.IsValid)
            {
                _context.Add(scheduledTrip);
                return RedirectToAction(nameof(Index));
            }
            ViewData["LorryId"] = new SelectList(_context.GetAllLorries(), "Id", "LorryBrand", scheduledTrip.LorryId);
            return View(scheduledTrip);
        }

        public IActionResult Edit(int id)
        {
            var scheduledTrip = _context.Get(id);
            ViewData["LorryId"] = new SelectList(_context.GetAllLorries(), "Id", "LorryBrand");
            return View(scheduledTrip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ScheduledTripId,LorryId,TripDescription,Distance,DateOfDepartue")] ScheduledTrips scheduledTrip)
        {
            Console.WriteLine(scheduledTrip);
            scheduledTrip.CreationTripDate = DateTime.Now;
            var lorry = _context.GetAllLorries().FirstOrDefault(x => x.Id == scheduledTrip.LorryId);
            scheduledTrip.DateOfArrival = CalcDate.CalcArrivalDate(scheduledTrip.DateOfDepartue, scheduledTrip.Distance, lorry.MaxSpeed, lorry.BreakInMinutes, lorry.BreakAfterRideInHours);
            var lorryTrips = _context.GetAll().Where(x => x.LorryId == scheduledTrip.LorryId && x.ScheduledTripId != scheduledTrip.ScheduledTripId);
            if (!TripDateValidator.IsTripDateValid(lorryTrips, scheduledTrip.DateOfDepartue, scheduledTrip.DateOfArrival))
            {
                ModelState.AddModelError("DatesOverlap", "Trip overlap with another trip!");
            }
            if (ModelState.IsValid)
            {
                _context.Update(id, scheduledTrip);
                return RedirectToAction(nameof(Index));
            }
            ViewData["LorryId"] = new SelectList(_context.GetAllLorries(), "Id", "LorryBrand", scheduledTrip.LorryId);
            return View(scheduledTrip);
        }

        public IActionResult Delete(int id)
        {
            return View(_context.Get(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _context.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
