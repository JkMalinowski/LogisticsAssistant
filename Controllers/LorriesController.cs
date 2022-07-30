using LogisticsAssistant.Models;
using LogisticsAssistant.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsAssistant.Controllers
{
    [Authorize]
    public class LorriesController : Controller
    {
        private readonly ILorriesRepository _lorriesRepository;

        public LorriesController(ILorriesRepository lorriesRepository)
        {
            _lorriesRepository = lorriesRepository;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_lorriesRepository.GetAll());
        }

        public IActionResult Details(int id)
        {
            return View(_lorriesRepository.Get(id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,LorryBrand,MaxSpeed,BreakInMinutes,BreakAfterRideInHours")] Lorries lorries)
        {
            if (ModelState.IsValid)
            {
                _lorriesRepository.Add(lorries);
                return RedirectToAction(nameof(Index));
            }
            return View(lorries);
        }

        public IActionResult Edit(int id)
        {
            return View(_lorriesRepository.Get(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,LorryBrand,MaxSpeed,BreakInMinutes,BreakAfterRideInHours")] Lorries lorries)
        {
            if (ModelState.IsValid)
            {
                _lorriesRepository.Update(id, lorries);
                return RedirectToAction(nameof(Index));
            }
            return View(lorries);
        }

        public IActionResult Delete(int id)
        {
            return View(_lorriesRepository.Get(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _lorriesRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
