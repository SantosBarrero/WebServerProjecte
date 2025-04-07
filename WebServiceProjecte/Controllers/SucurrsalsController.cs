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

        // GET: api/Sucurrsals
        [Route("api/Sucurrsals")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sucurrsal>>> GetSucur()
        {
            return await _context.Sucurrsals.OrderBy(x => x.SucurrsalId).ToListAsync();
        }

        // GET: api/Sucurrsals/5
        [Route("api/Sucurrsals/{id}")]
        [HttpGet]
        public async Task<ActionResult<Sucurrsal?>> GetSucursal(int id)
        {
            return await _context.Sucurrsals.Where(x => x.SucurrsalId == id).FirstOrDefaultAsync();
        }

        // PUT: api/Sucurrsals/5
        [Route("api/Sucurrsals/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutSucurrsal(int id, Sucurrsal s)
        {
            if (id != s.SucurrsalId)
            {
                return BadRequest();
            }

            _context.Entry(s).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SucurrsalExists(id))
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

        // POST: api/Sucurrsals
        [Route("api/Sucurrsals")]
        [HttpPost]
        public async Task<ActionResult<Usuari>> PostSucurrsal(Sucurrsal s)
        {
            _context.Sucurrsals.Add(s);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSucur), new { id = s.SucurrsalId }, s);
        }

        // DELETE: api/Sucurrsals/5
        [Route("api/Sucurrsals/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUsuari(int id)
        {
            var s = await _context.Sucurrsals.FindAsync(id);
            if (s == null)
            {
                return NotFound();
            }

            _context.Sucurrsals.Remove(s);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SucurrsalExists(int id)
        {
            return _context.Sucurrsals.Any(e => e.SucurrsalId == id);
        }
    }
}
