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
    public class LorriesController : Controller
    {
        private readonly LogisticsAssistantContext _context;

        public LorriesController(LogisticsAssistantContext context)
        {
            _context = context;
        }

        // GET: Lorries
        public async Task<IActionResult> Index()
        {
              return _context.Lorries != null ? 
                          View(await _context.Lorries.ToListAsync()) :
                          Problem("Entity set 'LogisticsAssistantContext.Lorries'  is null.");
        }

        // GET: Lorries/Details/5
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

        // GET: Lorries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lorries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LorryBrand,MaxSpeed,BreakInMinutes")] Lorries lorries)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lorries);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lorries);
        }

        // GET: Lorries/Edit/5
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

        // POST: Lorries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LorryBrand,MaxSpeed,BreakInMinutes")] Lorries lorries)
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

        // GET: Lorries/Delete/5
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

        // POST: Lorries/Delete/5
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
