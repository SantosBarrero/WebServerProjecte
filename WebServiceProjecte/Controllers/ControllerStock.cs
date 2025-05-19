using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
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

        [Route("api/Stock/Sucur")]
        [HttpPost]
        public async void PostStockSucursal([FromBody]Stock s)
        {

            await _context.Stocks.AddAsync(s);
            _context.SaveChanges();

        }
        [Route("api/Stock/Sucur")]
        [HttpPut]
        public async void PutStockSucursal([FromBody]Stock s)
        {
            _context.Stocks.Update(s);
            _context.SaveChanges();
        }
    }
}