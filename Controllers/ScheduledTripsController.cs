using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LogisticsAssistant.Data;
using LogisticsAssistant.Models;

namespace LogisticsAssistant.Controllers
{
    public class ScheduledTripsController : Controller
    {
        private readonly LogisticsAssistantContext _context;

        public ScheduledTripsController(LogisticsAssistantContext context)
        {
            _context = context;
        }

        // GET: ScheduledTrips
        public async Task<IActionResult> Index()
        {
            var logisticsAssistantContext = _context.ScheduledTrips.Include(s => s.Lorry);
            return View(await logisticsAssistantContext.ToListAsync());
        }

        // GET: ScheduledTrips/Details/5
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

        // GET: ScheduledTrips/Create
        public IActionResult Create()
        {
            ViewData["LorryId"] = new SelectList(_context.Lorries, "Id", "LorryBrand");
            //ViewData["LorryId"] = new SelectList(_context.Lorries, "Id", "LorryBrand");
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
                if(dateOfArrival.AddMinutes(breakTime) < trip.DateOfDepartue ||
                    trip.DateOfArrival.AddMinutes(breakTime) < dateOfDepartue)
                {
                    Console.WriteLine();
                    return false;
                }
            }
            return true;
        }

        // POST: ScheduledTrips/Create
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

        // GET: ScheduledTrips/Edit/5
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

        // POST: ScheduledTrips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: ScheduledTrips/Delete/5
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

        // POST: ScheduledTrips/Delete/5
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
