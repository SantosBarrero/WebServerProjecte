using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using WebServiceProjecte.Models;

namespace WebServiceProjecte.Controllers
{
    public class ControllerStock : Controller
    {
        public readonly GestióComerçContext _context;

        public ControllerStock(GestióComerçContext context)
        {
            this._context = context;
        }

        [Route("api/Stock/Sucur/{id}")]
        [HttpGet]
        public async Task<ActionResult<List<Stock>>> GetStockSucursal(int id)
        {

            return await _context.Stocks.Where(s => s.SucursalId == id).ToListAsync();
        }

        [Route("api/Stock")]
        [HttpPost]
        public async Task<ActionResult> PostStockSucursal([FromBody]Stock s)
        {
            try
            {
                await _context.Stocks.AddAsync(s);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e.Message}");
            }
            

        }
        [Route("api/Stock")]
        [HttpPut]
        public async void PutStockSucursal([FromBody]Stock s)
        {
            _context.Stocks.Update(s);
            _context.SaveChanges();
        }
        [Route("api/Stock/CodiDeBarres={codi}&SucursalId={id}")]
        [HttpDelete]
        public async Task<ActionResult> DelStock(string codi, int id)
        {
            Debug.WriteLine(codi);
            Debug.WriteLine(id);
            Stock s = _context.Stocks.Where(x => x.SucursalId == id && x.CodiDeBarres == codi).First();
            _context.Stocks.Remove(s);
            _context.SaveChanges();
            return Ok();
        }

    }
}