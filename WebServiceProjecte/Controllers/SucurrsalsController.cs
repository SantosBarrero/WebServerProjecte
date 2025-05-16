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
    public class SucursalsController : Controller
    {
        private readonly GestióComerçContext _context;

        public SucursalsController(GestióComerçContext context)
        {
            _context = context;
        }

        // GET: api/Sucursals
        [Route("api/Sucursals")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sucursal>>> GetSucur()
        {
            return await _context.Sucursals.OrderBy(x => x.SucursalId).ToListAsync();
        }

        // GET: api/Sucursals/5
        [Route("api/Sucursals/{id}")]
        [HttpGet]
        public async Task<ActionResult<Sucursal?>> GetSucursal(int id)
        {
            return await _context.Sucursals.Where(x => x.SucursalId == id).FirstOrDefaultAsync();
        }

        [Route("api/Sucursals/Comerç/{id}")]
        [HttpGet]
        public async Task<ActionResult<List<Sucursal>>> GetSucursalsComerç(int id)
        {
            return await _context.Sucursals.Where(x=>x.ComerçId == id).OrderBy(x => x.SucursalId).ToListAsync();
        }

        // PUT: api/Sucursals/5
        [Route("api/Sucursals/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutSucursal(int id, Sucursal s)
        {
            if (id != s.SucursalId)
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
                if (!SucursalExists(id))
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

        // POST: api/Sucursals
        [Route("api/Sucursals")]
        [HttpPost]
        public async Task<ActionResult<Sucursal>> PostSucursal([FromBody]Sucursal s)
        {
            int lastId = _context.Sucursals.Select(a => a.SucursalId).OrderByDescending(a => a).FirstOrDefault();
            s.SucursalId = lastId + 1;
            _context.Sucursals.Add(s);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSucur), new { id = s.SucursalId }, s);
        }

        // DELETE: api/Sucursals/5
        [Route("api/Sucursals/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUsuari(int id)
        {
            var s = await _context.Sucursals.FindAsync(id);
            if (s == null)
            {
                return NotFound();
            }

            _context.Sucursals.Remove(s);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SucursalExists(int id)
        {
            return _context.Sucursals.Any(e => e.SucursalId == id);
        }
    }
}
