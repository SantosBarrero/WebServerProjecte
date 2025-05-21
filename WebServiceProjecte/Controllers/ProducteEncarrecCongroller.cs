
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
        public async Task<ActionResult<ICollection<ProducteEncarrec>>> GetProductesEncarrec(int id)
        {
            return await _context.ProducteEncarrecs.Where(x=>x.EncarrecId == id).ToListAsync();
        }
        [Route("api/ProducteEncarrec/Productes/{id}")]
        [HttpGet]
        public async Task<ActionResult<List<Producte>>> GetProductes(int id)
        {
            List<string> productes = _context.ProducteEncarrecs.Where(x => x.EncarrecId == id).Select(x => x.CodiDeBarres).ToList();
            return await _context.Productes.Where(p => productes.Contains(p.CodiDeBarres)).ToListAsync();
        }
        [Route ("api/ProducteEncarrec")]
        [HttpPost]
        public async Task PostProducteEncarrec([FromBody] ProducteEncarrec pe)
        {
            _context.ProducteEncarrecs.Add(pe);
            await _context.SaveChangesAsync();
        }
        [Route ("api/ProducteEncarrec/{id}")]
        [HttpDelete]
        public async Task DeleteProducteEncarrec(int id)
        {
            await _context.ProducteEncarrecs.Where(x => x.EncarrecId == id).ExecuteDeleteAsync();
        }
    }
}
