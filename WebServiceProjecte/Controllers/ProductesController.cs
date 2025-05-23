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
    public class ProductesController : Controller
    {
        private readonly GestióComerçContext _context;

        public ProductesController(GestióComerçContext context)
        {
            _context = context;
        }

        // GET: api/Productes
        [Route("api/Productes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producte>>> GetProd()
        {
            return await _context.Productes.OrderBy(x => x.Nom).ToListAsync();
        }
        [Route("api/Productes/{codi}")]
        [HttpGet]
        public async Task<ActionResult<Producte?>> GetProducte(string codi)
        {
            return await _context.Productes.Where(p => p.CodiDeBarres == codi).FirstOrDefaultAsync();
        }

        // PUT: api/Productes/5
        [Route("api/Productes/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutProd(string id, [FromBody]Producte p)
        {
            if (id != p.CodiDeBarres)
            {
                return BadRequest();
            }

            _context.Entry(p).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProducteExists(id))
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

        // POST: api/Productes
        [Route("api/Productes")]
        [HttpPost]
        public async Task<ActionResult<Producte?>> PostProd([FromBody]Producte p)
        {
            try
            {
                _context.Productes.Add(p);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProd), new { id = p.CodiDeBarres }, p);
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e.Message} \n {e.InnerException}");
            }
            
        }

        // DELETE: api/Productes/5
        [Route("api/Productes/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteProd(string id)
        {
            var p = await _context.Productes.FindAsync(id);
            if (p == null)
            {
                return NotFound();
            }

            _context.Productes.Remove(p);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProducteExists(string id)
        {
            return _context.Productes.Any(e => e.CodiDeBarres == id);
        }
    }
}
