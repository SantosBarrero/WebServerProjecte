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
        public async void PutStockSucursal(string codiBarres, int sucurId, int stock)
        {

            Stock s = new Stock();
            s.SucursalId = sucurId;
            s.CodiDeBarres = codiBarres;
            s.Stock1 = stock;
            _context.Stocks.Add(s);
            _context.SaveChanges();

        }
    }
}