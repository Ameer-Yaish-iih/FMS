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
    public class RectangleGeofencesController : Controller
    {
        private readonly AnasFmsContext _context;

        public RectangleGeofencesController(AnasFmsContext context)
        {
            _context = context;
        }

        // GET: RectangleGeofences
        public async Task<IActionResult> Index()
        {
            var anasFmsContext = _context.RectangleGeofences.Include(r => r.Geofence);
            return View(await anasFmsContext.ToListAsync());
        }

        // GET: RectangleGeofences/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.RectangleGeofences == null)
            {
                return NotFound();
            }

            var rectangleGeofence = await _context.RectangleGeofences
                .Include(r => r.Geofence)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rectangleGeofence == null)
            {
                return NotFound();
            }

            return View(rectangleGeofence);
        }

        // GET: RectangleGeofences/Create
        public IActionResult Create()
        {
            ViewData["GeofenceId"] = new SelectList(_context.Geofences, "GeofenceId", "GeofenceId");
            return View();
        }

        // POST: RectangleGeofences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GeofenceId,North,East,West,South")] RectangleGeofence rectangleGeofence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rectangleGeofence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeofenceId"] = new SelectList(_context.Geofences, "GeofenceId", "GeofenceId", rectangleGeofence.GeofenceId);
            return View(rectangleGeofence);
        }

        // GET: RectangleGeofences/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.RectangleGeofences == null)
            {
                return NotFound();
            }

            var rectangleGeofence = await _context.RectangleGeofences.FindAsync(id);
            if (rectangleGeofence == null)
            {
                return NotFound();
            }
            ViewData["GeofenceId"] = new SelectList(_context.Geofences, "GeofenceId", "GeofenceId", rectangleGeofence.GeofenceId);
            return View(rectangleGeofence);
        }

        // POST: RectangleGeofences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,GeofenceId,North,East,West,South")] RectangleGeofence rectangleGeofence)
        {
            if (id != rectangleGeofence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rectangleGeofence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RectangleGeofenceExists(rectangleGeofence.Id))
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
            ViewData["GeofenceId"] = new SelectList(_context.Geofences, "GeofenceId", "GeofenceId", rectangleGeofence.GeofenceId);
            return View(rectangleGeofence);
        }

        // GET: RectangleGeofences/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.RectangleGeofences == null)
            {
                return NotFound();
            }

            var rectangleGeofence = await _context.RectangleGeofences
                .Include(r => r.Geofence)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rectangleGeofence == null)
            {
                return NotFound();
            }

            return View(rectangleGeofence);
        }

        // POST: RectangleGeofences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.RectangleGeofences == null)
            {
                return Problem("Entity set 'AnasFmsContext.RectangleGeofences'  is null.");
            }
            var rectangleGeofence = await _context.RectangleGeofences.FindAsync(id);
            if (rectangleGeofence != null)
            {
                _context.RectangleGeofences.Remove(rectangleGeofence);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RectangleGeofenceExists(long id)
        {
          return (_context.RectangleGeofences?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
