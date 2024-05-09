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
    public class PolygonGeofencesController : Controller
    {
        private readonly AnasFmsContext _context;

        public PolygonGeofencesController(AnasFmsContext context)
        {
            _context = context;
        }

        // GET: PolygonGeofences
        public async Task<IActionResult> Index()
        {
            var anasFmsContext = _context.PolygonGeofences.Include(p => p.Geofence);
            return View(await anasFmsContext.ToListAsync());
        }

        // GET: PolygonGeofences/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.PolygonGeofences == null)
            {
                return NotFound();
            }

            var polygonGeofence = await _context.PolygonGeofences
                .Include(p => p.Geofence)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (polygonGeofence == null)
            {
                return NotFound();
            }

            return View(polygonGeofence);
        }

        // GET: PolygonGeofences/Create
        public IActionResult Create()
        {
            ViewData["GeofenceId"] = new SelectList(_context.Geofences, "GeofenceId", "GeofenceId");
            return View();
        }

        // POST: PolygonGeofences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GeofenceId,Latitude,Longitude")] PolygonGeofence polygonGeofence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(polygonGeofence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeofenceId"] = new SelectList(_context.Geofences, "GeofenceId", "GeofenceId", polygonGeofence.GeofenceId);
            return View(polygonGeofence);
        }

        // GET: PolygonGeofences/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.PolygonGeofences == null)
            {
                return NotFound();
            }

            var polygonGeofence = await _context.PolygonGeofences.FindAsync(id);
            if (polygonGeofence == null)
            {
                return NotFound();
            }
            ViewData["GeofenceId"] = new SelectList(_context.Geofences, "GeofenceId", "GeofenceId", polygonGeofence.GeofenceId);
            return View(polygonGeofence);
        }

        // POST: PolygonGeofences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,GeofenceId,Latitude,Longitude")] PolygonGeofence polygonGeofence)
        {
            if (id != polygonGeofence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(polygonGeofence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolygonGeofenceExists(polygonGeofence.Id))
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
            ViewData["GeofenceId"] = new SelectList(_context.Geofences, "GeofenceId", "GeofenceId", polygonGeofence.GeofenceId);
            return View(polygonGeofence);
        }

        // GET: PolygonGeofences/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.PolygonGeofences == null)
            {
                return NotFound();
            }

            var polygonGeofence = await _context.PolygonGeofences
                .Include(p => p.Geofence)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (polygonGeofence == null)
            {
                return NotFound();
            }

            return View(polygonGeofence);
        }

        // POST: PolygonGeofences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.PolygonGeofences == null)
            {
                return Problem("Entity set 'AnasFmsContext.PolygonGeofences'  is null.");
            }
            var polygonGeofence = await _context.PolygonGeofences.FindAsync(id);
            if (polygonGeofence != null)
            {
                _context.PolygonGeofences.Remove(polygonGeofence);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolygonGeofenceExists(long id)
        {
          return (_context.PolygonGeofences?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
