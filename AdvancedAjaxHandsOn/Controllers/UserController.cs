using AdvancedAjaxHandsOn.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdvancedAjaxHandsOn.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UserController(AppDbContext context) => _context = context;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id) =>
            Ok(await _context.Users.FindAsync(id));

        [HttpGet("{id}/orders")]
        public async Task<IActionResult> GetUserOrders(int id) =>
            Ok(await _context.Orders.Where(o => o.UserId == id).ToListAsync());
    }
}
