using LogisticsAssistant.Data;
using LogisticsAssistant.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LogisticsAssistant.Controllers
{
    [Authorize]
    public class LorriesController : Controller
    {
        private readonly LogisticsAssistantContext _context;

        public LorriesController(LogisticsAssistantContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
              return _context.Lorries != null ?
                          View(await _context.Lorries.ToListAsync()) :
                          Problem("Entity set 'LogisticsAssistantContext.Lorries'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lorries == null)
            {
                return NotFound();
            }
            var lorries = await _context.Lorries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lorries == null)
            {
                return NotFound();
            }

            return View(lorries);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LorryBrand,MaxSpeed,BreakInMinutes,BreakAfterRideInHours")] Lorries lorries)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lorries);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lorries);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lorries == null)
            {
                return NotFound();
            }
            var lorries = await _context.Lorries.FindAsync(id);
            if (lorries == null)
            {
                return NotFound();
            }
            return View(lorries);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LorryBrand,MaxSpeed,BreakInMinutes,BreakAfterRideInHours")] Lorries lorries)
        {
            if (id != lorries.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lorries);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LorriesExists(lorries.Id))
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
            return View(lorries);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lorries == null)
            {
                return NotFound();
            }
            var lorries = await _context.Lorries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lorries == null)
            {
                return NotFound();
            }

            return View(lorries);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lorries == null)
            {
                return Problem("Entity set 'LogisticsAssistantContext.Lorries'  is null.");
            }
            var lorries = await _context.Lorries.FindAsync(id);
            if (lorries != null)
            {
                _context.Lorries.Remove(lorries);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LorriesExists(int id)
        {
          return (_context.Lorries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
