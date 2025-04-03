using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebServiceProjecte.Models;

namespace WebServiceProjecte.Controllers
{
    public class SucurrsalsController : Controller
    {
        private readonly GestióComerçContext _context;

        public SucurrsalsController(GestióComerçContext context)
        {
            _context = context;
        }

        // GET: Sucurrsals
        public async Task<IActionResult> Index()
        {
            var gestióComerçContext = _context.Sucurrsals.Include(s => s.Comerç);
            return View(await gestióComerçContext.ToListAsync());
        }

        // GET: Sucurrsals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucurrsal = await _context.Sucurrsals
                .Include(s => s.Comerç)
                .FirstOrDefaultAsync(m => m.SucurrsalId == id);
            if (sucurrsal == null)
            {
                return NotFound();
            }

            return View(sucurrsal);
        }

        // GET: Sucurrsals/Create
        public IActionResult Create()
        {
            ViewData["ComerçId"] = new SelectList(_context.Comerçs, "ComerçId", "ComerçId");
            return View();
        }

        // POST: Sucurrsals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SucurrsalId,Direccio,ComerçId")] Sucurrsal sucurrsal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sucurrsal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComerçId"] = new SelectList(_context.Comerçs, "ComerçId", "ComerçId", sucurrsal.ComerçId);
            return View(sucurrsal);
        }

        // GET: Sucurrsals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucurrsal = await _context.Sucurrsals.FindAsync(id);
            if (sucurrsal == null)
            {
                return NotFound();
            }
            ViewData["ComerçId"] = new SelectList(_context.Comerçs, "ComerçId", "ComerçId", sucurrsal.ComerçId);
            return View(sucurrsal);
        }

        // POST: Sucurrsals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SucurrsalId,Direccio,ComerçId")] Sucurrsal sucurrsal)
        {
            if (id != sucurrsal.SucurrsalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sucurrsal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SucurrsalExists(sucurrsal.SucurrsalId))
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
            ViewData["ComerçId"] = new SelectList(_context.Comerçs, "ComerçId", "ComerçId", sucurrsal.ComerçId);
            return View(sucurrsal);
        }

        // GET: Sucurrsals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucurrsal = await _context.Sucurrsals
                .Include(s => s.Comerç)
                .FirstOrDefaultAsync(m => m.SucurrsalId == id);
            if (sucurrsal == null)
            {
                return NotFound();
            }

            return View(sucurrsal);
        }

        // POST: Sucurrsals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sucurrsal = await _context.Sucurrsals.FindAsync(id);
            if (sucurrsal != null)
            {
                _context.Sucurrsals.Remove(sucurrsal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SucurrsalExists(int id)
        {
            return _context.Sucurrsals.Any(e => e.SucurrsalId == id);
        }
    }
}
