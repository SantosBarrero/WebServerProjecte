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
    public class UsuarisController : Controller
    {
        private readonly GestióComerçContext _context;

        public UsuarisController(GestióComerçContext context)
        {
            _context = context;
        }

        // GET: Usuaris
        public async Task<IActionResult> Index()
        {
            var gestióComerçContext = _context.Usuaris.Include(u => u.Comerç).Include(u => u.Sucurrsal);
            return View(await gestióComerçContext.ToListAsync());
        }

        // GET: Usuaris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuari = await _context.Usuaris
                .Include(u => u.Comerç)
                .Include(u => u.Sucurrsal)
                .FirstOrDefaultAsync(m => m.UsuId == id);
            if (usuari == null)
            {
                return NotFound();
            }

            return View(usuari);
        }

        // GET: Usuaris/Create
        public IActionResult Create()
        {
            ViewData["ComerçId"] = new SelectList(_context.Comerçs, "ComerçId", "ComerçId");
            ViewData["SucurrsalId"] = new SelectList(_context.Sucurrsals, "SucurrsalId", "SucurrsalId");
            return View();
        }

        // POST: Usuaris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuId,NomUsuari,Correu,Contrasenya,Rol,SucurrsalId,ComerçId")] Usuari usuari)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuari);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComerçId"] = new SelectList(_context.Comerçs, "ComerçId", "ComerçId", usuari.ComerçId);
            ViewData["SucurrsalId"] = new SelectList(_context.Sucurrsals, "SucurrsalId", "SucurrsalId", usuari.SucurrsalId);
            return View(usuari);
        }

        // GET: Usuaris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuari = await _context.Usuaris.FindAsync(id);
            if (usuari == null)
            {
                return NotFound();
            }
            ViewData["ComerçId"] = new SelectList(_context.Comerçs, "ComerçId", "ComerçId", usuari.ComerçId);
            ViewData["SucurrsalId"] = new SelectList(_context.Sucurrsals, "SucurrsalId", "SucurrsalId", usuari.SucurrsalId);
            return View(usuari);
        }

        // POST: Usuaris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsuId,NomUsuari,Correu,Contrasenya,Rol,SucurrsalId,ComerçId")] Usuari usuari)
        {
            if (id != usuari.UsuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuari);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariExists(usuari.UsuId))
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
            ViewData["ComerçId"] = new SelectList(_context.Comerçs, "ComerçId", "ComerçId", usuari.ComerçId);
            ViewData["SucurrsalId"] = new SelectList(_context.Sucurrsals, "SucurrsalId", "SucurrsalId", usuari.SucurrsalId);
            return View(usuari);
        }

        // GET: Usuaris/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuari = await _context.Usuaris
                .Include(u => u.Comerç)
                .Include(u => u.Sucurrsal)
                .FirstOrDefaultAsync(m => m.UsuId == id);
            if (usuari == null)
            {
                return NotFound();
            }

            return View(usuari);
        }

        // POST: Usuaris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuari = await _context.Usuaris.FindAsync(id);
            if (usuari != null)
            {
                _context.Usuaris.Remove(usuari);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuariExists(int id)
        {
            return _context.Usuaris.Any(e => e.UsuId == id);
        }
    }
}
