using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FMS.Models.EfModels;

namespace FMS.Controllers
{
    public class RouteHistoriesController : Controller
    {
        private readonly AnasFmsContext _context;

        public RouteHistoriesController(AnasFmsContext context)
        {
            _context = context;
        }

        // GET: RouteHistories
        public async Task<IActionResult> Index()
        {
            var anasFmsContext = _context.RouteHistories.Include(r => r.Vehicle);
            return View(await anasFmsContext.ToListAsync());
        }

        // GET: RouteHistories/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.RouteHistories == null)
            {
                return NotFound();
            }

            var routeHistory = await _context.RouteHistories
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (routeHistory == null)
            {
                return NotFound();
            }

            return View(routeHistory);
        }

        // GET: RouteHistories/Create
        public IActionResult Create()
        {
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "VehicleId");
            return View();
        }

        // POST: RouteHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RouteHistoryId,VehicleId,VehicleDirection,Status,VehicleSpeed,Epoch,Latitude,Longitude")] RouteHistory routeHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(routeHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "VehicleId", routeHistory.VehicleId);
            return View(routeHistory);
        }

        // GET: RouteHistories/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.RouteHistories == null)
            {
                return NotFound();
            }

            var routeHistory = await _context.RouteHistories.FindAsync(id);
            if (routeHistory == null)
            {
                return NotFound();
            }
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "VehicleId", routeHistory.VehicleId);
            return View(routeHistory);
        }

        // POST: RouteHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("RouteHistoryId,VehicleId,VehicleDirection,Status,VehicleSpeed,Epoch,Latitude,Longitude")] RouteHistory routeHistory)
        {
            if (id != routeHistory.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(routeHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteHistoryExists(routeHistory.VehicleId))
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
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "VehicleId", routeHistory.VehicleId);
            return View(routeHistory);
        }

        // GET: RouteHistories/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.RouteHistories == null)
            {
                return NotFound();
            }

            var routeHistory = await _context.RouteHistories
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (routeHistory == null)
            {
                return NotFound();
            }

            return View(routeHistory);
        }

        // POST: RouteHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.RouteHistories == null)
            {
                return Problem("Entity set 'AnasFmsContext.RouteHistories'  is null.");
            }
            var routeHistory = await _context.RouteHistories.FindAsync(id);
            if (routeHistory != null)
            {
                _context.RouteHistories.Remove(routeHistory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteHistoryExists(long id)
        {
          return (_context.RouteHistories?.Any(e => e.VehicleId == id)).GetValueOrDefault();
        }
    }
}
