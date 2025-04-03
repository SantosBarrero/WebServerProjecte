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
    public class EncarrecsController : Controller
    {
        private readonly GestióComerçContext _context;

        public EncarrecsController(GestióComerçContext context)
        {
            _context = context;
        }

        // GET: Encarrecs
        public async Task<IActionResult> Index()
        {
            var gestióComerçContext = _context.Encarrecs.Include(e => e.Sucurrsal).Include(e => e.Usu);
            return View(await gestióComerçContext.ToListAsync());
        }

        // GET: Encarrecs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encarrec = await _context.Encarrecs
                .Include(e => e.Sucurrsal)
                .Include(e => e.Usu)
                .FirstOrDefaultAsync(m => m.EncarrecId == id);
            if (encarrec == null)
            {
                return NotFound();
            }

            return View(encarrec);
        }

        // GET: Encarrecs/Create
        public IActionResult Create()
        {
            ViewData["SucurrsalId"] = new SelectList(_context.Sucurrsals, "SucurrsalId", "SucurrsalId");
            ViewData["UsuId"] = new SelectList(_context.Usuaris, "UsuId", "UsuId");
            return View();
        }

        // POST: Encarrecs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EncarrecId,PreuTotal,Data,Pagat,Estat,SucurrsalId,UsuId")] Encarrec encarrec)
        {
            if (ModelState.IsValid)
            {
                _context.Add(encarrec);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SucurrsalId"] = new SelectList(_context.Sucurrsals, "SucurrsalId", "SucurrsalId", encarrec.SucurrsalId);
            ViewData["UsuId"] = new SelectList(_context.Usuaris, "UsuId", "UsuId", encarrec.UsuId);
            return View(encarrec);
        }

        // GET: Encarrecs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encarrec = await _context.Encarrecs.FindAsync(id);
            if (encarrec == null)
            {
                return NotFound();
            }
            ViewData["SucurrsalId"] = new SelectList(_context.Sucurrsals, "SucurrsalId", "SucurrsalId", encarrec.SucurrsalId);
            ViewData["UsuId"] = new SelectList(_context.Usuaris, "UsuId", "UsuId", encarrec.UsuId);
            return View(encarrec);
        }

        // POST: Encarrecs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EncarrecId,PreuTotal,Data,Pagat,Estat,SucurrsalId,UsuId")] Encarrec encarrec)
        {
            if (id != encarrec.EncarrecId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(encarrec);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EncarrecExists(encarrec.EncarrecId))
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
            ViewData["SucurrsalId"] = new SelectList(_context.Sucurrsals, "SucurrsalId", "SucurrsalId", encarrec.SucurrsalId);
            ViewData["UsuId"] = new SelectList(_context.Usuaris, "UsuId", "UsuId", encarrec.UsuId);
            return View(encarrec);
        }

        // GET: Encarrecs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encarrec = await _context.Encarrecs
                .Include(e => e.Sucurrsal)
                .Include(e => e.Usu)
                .FirstOrDefaultAsync(m => m.EncarrecId == id);
            if (encarrec == null)
            {
                return NotFound();
            }

            return View(encarrec);
        }

        // POST: Encarrecs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var encarrec = await _context.Encarrecs.FindAsync(id);
            if (encarrec != null)
            {
                _context.Encarrecs.Remove(encarrec);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EncarrecExists(int id)
        {
            return _context.Encarrecs.Any(e => e.EncarrecId == id);
        }
    }
}
