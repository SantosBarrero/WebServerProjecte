using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<ICollection<Producte>>> GetStockSucursal(int id)
        {
            var sucursal = _context.Sucursals.Where(s => s.SucursalId == id).FirstOrDefault();
            return await _context.Productes.Where(p => p.Sucursals.Contains(sucursal)).ToListAsync();
        }

        [Route("api/Stock/Sucur")]
        [HttpPost]
        public async void PutStockSucursal(string codiBarres, int sucurId)
        {
            var sucursal = _context.Sucursals.Where(s => s.SucursalId == sucurId).FirstOrDefault();
            var producte = _context.Productes.Where(p => p.CodiDeBarres == codiBarres).FirstOrDefault();
            producte.Sucursals.Add(sucursal);
            _context.Productes.Update(producte);
            _context.SaveChanges();

        }
    }
}