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
    public class CircleGeofencesController : Controller
    {
        private readonly AnasFmsContext _context;

        public CircleGeofencesController(AnasFmsContext context)
        {
            _context = context;
        }

        // GET: CircleGeofences
        public async Task<IActionResult> Index()
        {
            var anasFmsContext = _context.CircleGeofences.Include(c => c.Geofence);
            return View(await anasFmsContext.ToListAsync());
        }

        // GET: CircleGeofences/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.CircleGeofences == null)
            {
                return NotFound();
            }

            var circleGeofence = await _context.CircleGeofences
                .Include(c => c.Geofence)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (circleGeofence == null)
            {
                return NotFound();
            }

            return View(circleGeofence);
        }

        // GET: CircleGeofences/Create
        public IActionResult Create()
        {
            ViewData["GeofenceId"] = new SelectList(_context.Geofences, "GeofenceId", "GeofenceId");
            return View();
        }

        // POST: CircleGeofences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GeofenceId,Radius,Latitude,Longitude")] CircleGeofence circleGeofence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(circleGeofence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeofenceId"] = new SelectList(_context.Geofences, "GeofenceId", "GeofenceId", circleGeofence.GeofenceId);
            return View(circleGeofence);
        }

        // GET: CircleGeofences/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.CircleGeofences == null)
            {
                return NotFound();
            }

            var circleGeofence = await _context.CircleGeofences.FindAsync(id);
            if (circleGeofence == null)
            {
                return NotFound();
            }
            ViewData["GeofenceId"] = new SelectList(_context.Geofences, "GeofenceId", "GeofenceId", circleGeofence.GeofenceId);
            return View(circleGeofence);
        }

        // POST: CircleGeofences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,GeofenceId,Radius,Latitude,Longitude")] CircleGeofence circleGeofence)
        {
            if (id != circleGeofence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(circleGeofence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CircleGeofenceExists(circleGeofence.Id))
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
            ViewData["GeofenceId"] = new SelectList(_context.Geofences, "GeofenceId", "GeofenceId", circleGeofence.GeofenceId);
            return View(circleGeofence);
        }

        // GET: CircleGeofences/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.CircleGeofences == null)
            {
                return NotFound();
            }

            var circleGeofence = await _context.CircleGeofences
                .Include(c => c.Geofence)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (circleGeofence == null)
            {
                return NotFound();
            }

            return View(circleGeofence);
        }

        // POST: CircleGeofences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.CircleGeofences == null)
            {
                return Problem("Entity set 'AnasFmsContext.CircleGeofences'  is null.");
            }
            var circleGeofence = await _context.CircleGeofences.FindAsync(id);
            if (circleGeofence != null)
            {
                _context.CircleGeofences.Remove(circleGeofence);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CircleGeofenceExists(long id)
        {
          return (_context.CircleGeofences?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
