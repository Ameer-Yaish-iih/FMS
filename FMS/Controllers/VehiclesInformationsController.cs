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
    public class VehiclesInformationsController : Controller
    {
        private readonly AnasFmsContext _context;

        public VehiclesInformationsController(AnasFmsContext context)
        {
            _context = context;
        }

        // GET: VehiclesInformations
        public async Task<IActionResult> Index()
        {
            var anasFmsContext = _context.VehiclesInformations.Include(v => v.Driver).Include(v => v.Vehicle);
            return View(await anasFmsContext.ToListAsync());
        }

        // GET: VehiclesInformations/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.VehiclesInformations == null)
            {
                return NotFound();
            }

            var vehiclesInformation = await _context.VehiclesInformations
                .Include(v => v.Driver)
                .Include(v => v.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehiclesInformation == null)
            {
                return NotFound();
            }

            return View(vehiclesInformation);
        }

        // GET: VehiclesInformations/Create
        public IActionResult Create()
        {
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "DriverId");
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "VehicleId");
            return View();
        }

        // POST: VehiclesInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VehicleId,DriverId,VehicleMake,VehicleModel,PurchaseDate")] VehiclesInformation vehiclesInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehiclesInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "DriverId", vehiclesInformation.DriverId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "VehicleId", vehiclesInformation.VehicleId);
            return View(vehiclesInformation);
        }

        // GET: VehiclesInformations/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.VehiclesInformations == null)
            {
                return NotFound();
            }

            var vehiclesInformation = await _context.VehiclesInformations.FindAsync(id);
            if (vehiclesInformation == null)
            {
                return NotFound();
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "DriverId", vehiclesInformation.DriverId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "VehicleId", vehiclesInformation.VehicleId);
            return View(vehiclesInformation);
        }

        // POST: VehiclesInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,VehicleId,DriverId,VehicleMake,VehicleModel,PurchaseDate")] VehiclesInformation vehiclesInformation)
        {
            if (id != vehiclesInformation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehiclesInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiclesInformationExists(vehiclesInformation.Id))
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
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "DriverId", vehiclesInformation.DriverId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "VehicleId", vehiclesInformation.VehicleId);
            return View(vehiclesInformation);
        }

        // GET: VehiclesInformations/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.VehiclesInformations == null)
            {
                return NotFound();
            }

            var vehiclesInformation = await _context.VehiclesInformations
                .Include(v => v.Driver)
                .Include(v => v.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehiclesInformation == null)
            {
                return NotFound();
            }

            return View(vehiclesInformation);
        }

        // POST: VehiclesInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.VehiclesInformations == null)
            {
                return Problem("Entity set 'AnasFmsContext.VehiclesInformations'  is null.");
            }
            var vehiclesInformation = await _context.VehiclesInformations.FindAsync(id);
            if (vehiclesInformation != null)
            {
                _context.VehiclesInformations.Remove(vehiclesInformation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiclesInformationExists(long id)
        {
          return (_context.VehiclesInformations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
