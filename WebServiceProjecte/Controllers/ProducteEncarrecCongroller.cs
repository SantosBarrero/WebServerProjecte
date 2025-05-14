
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServiceProjecte.Models;

namespace WebServiceProjecte.Controllers
{
    public class ProducteEncarrecCongroller : Controller
    {
        public readonly GestióComerçContext _context;
        public ProducteEncarrecCongroller(GestióComerçContext context)
        {
            this._context = context;
        }
        [Route("api/ProducteEncarrec/{id}")]
        [HttpGet]
        public async Task<ActionResult<ICollection<Producte>>> GetProductesEncarrec(int id)
        {
            var encarrec = _context.Encarrecs.Where(e => e.EncarrecId == id).FirstOrDefault();
            return await _context.Productes.Where(p => p.Encarrecs.Contains(encarrec)).ToListAsync();
        }
    }
}
