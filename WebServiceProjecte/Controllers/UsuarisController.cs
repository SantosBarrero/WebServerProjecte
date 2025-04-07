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

        // GET: api/Usuaris
        [Route("api/Usuaris")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuari>>> GetUsus()
        {
            return await _context.Usuaris.OrderBy(x => x.NomUsuari).ToListAsync();
        }
        // GET: api/Usuaris/5
        [Route("api/Usuaris/{id}")]
        [HttpGet]
        public async Task<ActionResult<Usuari?>> GetUsuari(int id)
        {
            return await _context.Usuaris.Where(x=>x.UsuId == id).FirstOrDefaultAsync();
        }

        // PUT: api/Usuaris/5
        [Route("api/Usuaris/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutUsuari(int id, Usuari usuari)
        {
            if (id != usuari.UsuId)
            {
                return BadRequest();
            }

            _context.Entry(usuari).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuaris
        [Route("api/Usuaris")]
        [HttpPost]
        public async Task<ActionResult<Usuari>> PostUsuari(Usuari usuari)
        {
            _context.Usuaris.Add(usuari);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsus), new { id = usuari.UsuId }, usuari);
        }

        // DELETE: api/Usuaris/5
        [Route("api/Usuaris/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUsuari(int id)
        {
            var usuari = await _context.Usuaris.FindAsync(id);
            if (usuari == null)
            {
                return NotFound();
            }

            _context.Usuaris.Remove(usuari);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuariExists(int id)
        {
            return _context.Usuaris.Any(e => e.UsuId == id);
        }
    }
}
