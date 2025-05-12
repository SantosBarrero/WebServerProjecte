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

        // GET: api/Comerç
        [Route("api/Comerç")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comerç>>> GetComerços()
        {
            return await _context.Comerçs.OrderBy(x => x.Nom).ToListAsync();
        }

        // GET: api/Comerç/5
        [Route("api/Comerç/{id}")]
        [HttpGet]
        public async Task<ActionResult<Comerç?>> GetComerç(int id)
        {
            return await _context.Comerçs.Where(x => x.ComerçId == id).FirstOrDefaultAsync();
        }
        // PUT: api/Comerç/5
        [Route("api/Comerç/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutComerç(int id, Comerç c)
        {
            if (id != c.ComerçId)
            {
                return BadRequest();
            }

            _context.Entry(c).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComerçExists(id))
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

        // POST: api/Comerç
        [Route("api/Comerç")]
        [HttpPost]
        public async Task<ActionResult<Comerç>> PostUsuari(Comerç c)
        {
            int lastId = _context.Comerçs.Select(a => a.ComerçId).OrderByDescending(a => a).FirstOrDefault();
            c.ComerçId = lastId + 1;
            _context.Comerçs.Add(c);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComerços), new { id = c.ComerçId }, c);
        }

        // DELETE: api/Comerç/5
        [Route("api/Comerç/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteComerç(int id)
        {
            var c = await _context.Comerçs.FindAsync(id);
            if (c == null)
            {
                return NotFound();
            }

            _context.Comerçs.Remove(c);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComerçExists(int id)
        {
            return _context.Comerçs.Any(e => e.ComerçId == id);
        }
    }
}