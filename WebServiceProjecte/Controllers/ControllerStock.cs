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
    }
}