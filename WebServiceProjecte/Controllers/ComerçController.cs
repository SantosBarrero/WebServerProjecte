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
    public class ComerçController : Controller
    {
        private readonly GestióComerçContext _context;

        public ComerçController(GestióComerçContext context)
        {
            _context = context;
        }

        // GET: Comerç
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comerçs.ToListAsync());
        }

        // GET: Comerç/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comerç = await _context.Comerçs
                .FirstOrDefaultAsync(m => m.ComerçId == id);
            if (comerç == null)
            {
                return NotFound();
            }

            return View(comerç);
        }

        // GET: Comerç/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Comerç/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComerçId,Nom,Telefon,Email,Nif")] Comerç comerç)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comerç);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comerç);
        }

        // GET: Comerç/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comerç = await _context.Comerçs.FindAsync(id);
            if (comerç == null)
            {
                return NotFound();
            }
            return View(comerç);
        }

        // POST: Comerç/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComerçId,Nom,Telefon,Email,Nif")] Comerç comerç)
        {
            if (id != comerç.ComerçId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comerç);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComerçExists(comerç.ComerçId))
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
            return View(comerç);
        }

        // GET: Comerç/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comerç = await _context.Comerçs
                .FirstOrDefaultAsync(m => m.ComerçId == id);
            if (comerç == null)
            {
                return NotFound();
            }

            return View(comerç);
        }

        // POST: Comerç/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comerç = await _context.Comerçs.FindAsync(id);
            if (comerç != null)
            {
                _context.Comerçs.Remove(comerç);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComerçExists(int id)
        {
            return _context.Comerçs.Any(e => e.ComerçId == id);
        }
    }
}
