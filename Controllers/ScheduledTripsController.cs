using LogisticsAssistant.Data;
using LogisticsAssistant.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LogisticsAssistant.Controllers
{
    [Authorize]
    public class ScheduledTripsController : Controller
    {
        private readonly LogisticsAssistantContext _context;

        public ScheduledTripsController(LogisticsAssistantContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string lorryBrand, string searchString)
        {
            var logisticsAssistantContext = _context.ScheduledTrips.Include(s => s.Lorry);
            if (!String.IsNullOrEmpty(searchString))
            {
                var logisticsAssistantContext2 = logisticsAssistantContext.Where(x => x.TripDescription.Contains(searchString));
                return View(await logisticsAssistantContext2.ToListAsync());
            }
            return View(await logisticsAssistantContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ScheduledTrips == null)
            {
                return NotFound();
            }

            var scheduledTrips = await _context.ScheduledTrips
                .Include(s => s.Lorry)
                .FirstOrDefaultAsync(m => m.ScheduledTripId == id);
            if (scheduledTrips == null)
            {
                return NotFound();
            }

            return View(scheduledTrips);
        }

        public IActionResult Create()
        {
            ViewData["LorryId"] = new SelectList(_context.Lorries, "Id", "LorryBrand");
            return View();
        }

        private DateTime CalcArrivalDate(DateTime departueDate, int distance, int maxSpeed)
        {
            double tripTimeInHours = distance / maxSpeed;
            DateTime arrivalDate = departueDate.AddHours(tripTimeInHours);
            return arrivalDate;
        }

        private bool IsTripDateValid(int lorryId, DateTime dateOfDepartue, DateTime dateOfArrival, int breakTime)
        {
            var lorryTrips = _context.ScheduledTrips.Where(x => x.LorryId == lorryId);
            foreach(var trip in lorryTrips)
            {
                if ((dateOfDepartue < trip.DateOfDepartue && dateOfArrival > trip.DateOfDepartue) ||
                    (dateOfDepartue < trip.DateOfArrival && dateOfArrival > trip.DateOfArrival) ||
                    (dateOfDepartue < trip.DateOfDepartue && dateOfArrival > trip.DateOfArrival) ||
                    (dateOfDepartue > trip.DateOfDepartue && dateOfArrival < trip.DateOfArrival) ||
                    dateOfArrival.AddMinutes(breakTime) > trip.DateOfDepartue ||
                    trip.DateOfArrival.AddMinutes(breakTime) < dateOfDepartue)
                {
                    Console.WriteLine("Dates overlap");
                    return false;
                }
            }
            return true;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LorryId,TripDescription,Distance,DateOfDepartue,DateOfArrival,CreationTripDate")] ScheduledTrips scheduledTrips)
        {
            scheduledTrips.CreationTripDate = DateTime.Now;
            var lorry = _context.Lorries.FirstOrDefaultAsync(x => x.Id == scheduledTrips.LorryId);
            scheduledTrips.DateOfArrival = CalcArrivalDate(scheduledTrips.DateOfDepartue, scheduledTrips.Distance, lorry.Result.MaxSpeed);
            if (ModelState.IsValid && IsTripDateValid(lorry.Result.Id, scheduledTrips.DateOfDepartue, scheduledTrips.DateOfArrival, lorry.Result.BreakInMinutes))
            {
                _context.Add(scheduledTrips);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LorryId"] = new SelectList(_context.Lorries, "Id", "LorryBrand", scheduledTrips.LorryId);
            return View(scheduledTrips);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ScheduledTrips == null)
            {
                return NotFound();
            }

            var scheduledTrips = await _context.ScheduledTrips.FindAsync(id);
            if (scheduledTrips == null)
            {
                return NotFound();
            }
            ViewData["LorryId"] = new SelectList(_context.Lorries, "Id", "LorryBrand", scheduledTrips.LorryId);
            return View(scheduledTrips);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduledTripId,LorryId,TripDescription,Distance,DateOfDepartue,DateOfArrival,CreationTripDate")] ScheduledTrips scheduledTrips)
        {
            if (id != scheduledTrips.ScheduledTripId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scheduledTrips);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduledTripsExists(scheduledTrips.ScheduledTripId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LorryId"] = new SelectList(_context.Lorries, "Id", "LorryBrand", scheduledTrips.LorryId);
            return View(scheduledTrips);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ScheduledTrips == null)
            {
                return NotFound();
            }

            var scheduledTrips = await _context.ScheduledTrips
                .Include(s => s.Lorry)
                .FirstOrDefaultAsync(m => m.ScheduledTripId == id);
            if (scheduledTrips == null)
            {
                return NotFound();
            }

            return View(scheduledTrips);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ScheduledTrips == null)
            {
                return Problem("Entity set 'LogisticsAssistantContext.ScheduledTrips'  is null.");
            }
            var scheduledTrips = await _context.ScheduledTrips.FindAsync(id);
            if (scheduledTrips != null)
            {
                _context.ScheduledTrips.Remove(scheduledTrips);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduledTripsExists(int id)
        {
          return (_context.ScheduledTrips?.Any(e => e.ScheduledTripId == id)).GetValueOrDefault();
        }
    }
}
