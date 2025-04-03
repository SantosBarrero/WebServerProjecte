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
    public class ProductesController : Controller
    {
        private readonly GestióComerçContext _context;

        public ProductesController(GestióComerçContext context)
        {
            _context = context;
        }

        // GET: Productes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Productes.ToListAsync());
        }

        // GET: Productes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producte = await _context.Productes
                .FirstOrDefaultAsync(m => m.CodiDeBarres == id);
            if (producte == null)
            {
                return NotFound();
            }

            return View(producte);
        }

        // GET: Productes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodiDeBarres,Nom,Imatge,Descripcio,Preu,Categoria")] Producte producte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producte);
        }

        // GET: Productes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producte = await _context.Productes.FindAsync(id);
            if (producte == null)
            {
                return NotFound();
            }
            return View(producte);
        }

        // POST: Productes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodiDeBarres,Nom,Imatge,Descripcio,Preu,Categoria")] Producte producte)
        {
            if (id != producte.CodiDeBarres)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProducteExists(producte.CodiDeBarres))
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
            return View(producte);
        }

        // GET: Productes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producte = await _context.Productes
                .FirstOrDefaultAsync(m => m.CodiDeBarres == id);
            if (producte == null)
            {
                return NotFound();
            }

            return View(producte);
        }

        // POST: Productes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var producte = await _context.Productes.FindAsync(id);
            if (producte != null)
            {
                _context.Productes.Remove(producte);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProducteExists(string id)
        {
            return _context.Productes.Any(e => e.CodiDeBarres == id);
        }
    }
}
