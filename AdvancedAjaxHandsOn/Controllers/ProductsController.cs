using AdvancedAjaxHandsOn.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdvancedAjaxHandsOn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductsController(AppDbContext context) => _context = context;

        [HttpGet("search")]
        public async Task<IActionResult> Search(string q) =>
            Ok(await _context.Products.Where(p => p.Name.Contains(q)).ToListAsync());

        [HttpGet("scrollpage")]
        public async Task<IActionResult> GetProducts(int page = 1, string category = null, int? minPrice = null, int? maxPrice = null, string sortBy = null, string search = null)
        {
            int pageSize = 2;
            var query = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(search)) query = query.Where(p => p.Name.Contains(search));
            if (!string.IsNullOrEmpty(category)) query = query.Where(p => p.Category == category);
            if (minPrice.HasValue) query = query.Where(p => p.Price >= minPrice);
            if (maxPrice.HasValue) query = query.Where(p => p.Price <= maxPrice);
            query = sortBy == "price" ? query.OrderBy(p => p.Price) : query.OrderBy(p => p.Name);
            return Ok(await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync());
        }

        [HttpGet]
        public IActionResult GetPaged(int page = 1, int pageSize = 5)
        {
            var products = _context.Products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(products);
        }

    }
}
