using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tashtibaat.Data;
using Tashtibaat.Models;

namespace Tashtibaat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PreviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Users> _userManager;

        public PreviewsController(ApplicationDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Previews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Preview>>> GetPreviews()
        {
            return await _context.Previews.ToListAsync();
        }

        // GET: api/Previews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Preview>> GetPreview(int id)
        {
            var preview = await _context.Previews.FindAsync(id);

            if (preview == null)
            {
                return NotFound();
            }

            return preview;
        }

        // PUT: api/Previews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPreview(int id, Preview preview)
        {
            if (id != preview.Id)
            {
                return BadRequest();
            }

            _context.Entry(preview).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreviewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Previews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Preview>> PostPreview(PreviewDto dto)
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByNameAsync(userName);
            var preview = new Preview 
            { 
                Address = dto.Address,
                Notes = dto.Notes,
                Type = dto.Type ,
                Bank = dto.Bank,
                Cash = dto.Cash,
                Status = dto.Status,
                Date = dto.Date,
                User = user,
            };
            await _context.Previews.AddAsync(preview);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Data = new { },
                Status = true,
                Message = "تم ارسال طلبك بنجاح سوف نرد عليك في اقرب وقت"
            });
        }

        // DELETE: api/Previews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePreview(int id)
        {
            var preview = await _context.Previews.FindAsync(id);
            if (preview == null)
            {
                return NotFound();
            }

            _context.Previews.Remove(preview);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PreviewExists(int id)
        {
            return _context.Previews.Any(e => e.Id == id);
        }
    }
}
