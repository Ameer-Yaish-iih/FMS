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
    public class GeofencesController : Controller
    {
        private readonly AnasFmsContext _context;

        public GeofencesController(AnasFmsContext context)
        {
            _context = context;
        }

        // GET: Geofences
        public async Task<IActionResult> Index()
        {
              return _context.Geofences != null ? 
                          View(await _context.Geofences.ToListAsync()) :
                          Problem("Entity set 'AnasFmsContext.Geofences'  is null.");
        }

        // GET: Geofences/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Geofences == null)
            {
                return NotFound();
            }

            var geofence = await _context.Geofences
                .FirstOrDefaultAsync(m => m.GeofenceId == id);
            if (geofence == null)
            {
                return NotFound();
            }

            return View(geofence);
        }

        // GET: Geofences/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Geofences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GeofenceId,GeofenceType,AddedDate,StrockColor,StrockOpacity,StrockWeight,FillColor,FillOpacity")] Geofence geofence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(geofence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(geofence);
        }

        // GET: Geofences/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Geofences == null)
            {
                return NotFound();
            }

            var geofence = await _context.Geofences.FindAsync(id);
            if (geofence == null)
            {
                return NotFound();
            }
            return View(geofence);
        }

        // POST: Geofences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("GeofenceId,GeofenceType,AddedDate,StrockColor,StrockOpacity,StrockWeight,FillColor,FillOpacity")] Geofence geofence)
        {
            if (id != geofence.GeofenceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(geofence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeofenceExists(geofence.GeofenceId))
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
            return View(geofence);
        }

        // GET: Geofences/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Geofences == null)
            {
                return NotFound();
            }

            var geofence = await _context.Geofences
                .FirstOrDefaultAsync(m => m.GeofenceId == id);
            if (geofence == null)
            {
                return NotFound();
            }

            return View(geofence);
        }

        // POST: Geofences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Geofences == null)
            {
                return Problem("Entity set 'AnasFmsContext.Geofences'  is null.");
            }
            var geofence = await _context.Geofences.FindAsync(id);
            if (geofence != null)
            {
                _context.Geofences.Remove(geofence);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeofenceExists(long id)
        {
          return (_context.Geofences?.Any(e => e.GeofenceId == id)).GetValueOrDefault();
        }
    }
}
