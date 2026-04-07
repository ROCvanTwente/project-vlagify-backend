using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactMessage message)
        {
            _context.ContactMessages.Add(message);
            await _context.SaveChangesAsync();

            return Ok(new { success = true });
        }
    }
}