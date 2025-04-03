﻿using System;
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

        // GET: api/Encarrecs
        [Route("api/Encarrecs")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Encarrec>>> GetEncarrecs()
        {
            return await _context.Encarrecs.OrderBy(x => x.EncarrecId).ToListAsync();
        }

        // PUT: api/Encarrecs/5
        [Route("api/Encarrecs/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutEncarrecs(int id, Encarrec e)
        {
            if (id != e.EncarrecId)
            {
                return BadRequest();
            }

            _context.Entry(e).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EncarrecExists(id))
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

        // POST: api/Encarrecs
        [Route("api/Encarrecs")]
        [HttpPost]
        public async Task<ActionResult<Usuari>> PostEncarrec(Encarrec e)
        {
            _context.Encarrecs.Add(e);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEncarrecs), new { id = e.EncarrecId }, e);
        }

        // DELETE: api/Encarrecs/5
        [Route("api/Encarrecs/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEncarrec(int id)
        {
            var e = await _context.Encarrecs.FindAsync(id);
            if (e == null)
            {
                return NotFound();
            }

            _context.Encarrecs.Remove(e);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EncarrecExists(int id)
        {
            return _context.Encarrecs.Any(e => e.EncarrecId == id);
        }
    }
}
