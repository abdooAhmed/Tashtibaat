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
using Tashtibaat.Helpers;
using Tashtibaat.Models;

namespace Tashtibaat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssaysController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Users> _userManager;
        private readonly ISendMail _sendMail;

        public AssaysController(ApplicationDbContext context, UserManager<Users> userManager,ISendMail sendMail)
        {
            _context = context;
            _userManager = userManager;
            _sendMail = sendMail;
        }
        // GET: api/Assays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assay>>> GetAssays()
        {
            var assays = await _context.Assays.Select(x => new {x.Name,x.Id,x.MinimumPrice,x.MaximumPrice,x.Description,x.Unit}).ToListAsync();
            return Ok(new
            {
                Data = assays,
                Status = true,
                Message = "Success"
            });
        }

        // GET: api/Assays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Assay>> GetAssay(int id)
        {
            var assay = await _context.Assays.FindAsync(id);

            if (assay == null)
            {
                return NotFound();
            }

            return assay;
        }
        

        // PUT: api/Assays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssay(int id, AssayDto assayDto)
        {
            var assay = new Assay
            {
                Id= id,
                Description = assayDto.Description,
                MaximumPrice = assayDto.MaximumPrice,
                MinimumPrice = assayDto.MinimumPrice,
                Name = assayDto.Name,
                Unit = assayDto.Unit,
            };
            if (id != assay.Id)
            {
                return BadRequest();
            }

            _context.Entry(assay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssayExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new
            {
                Data = new { },
                Status = true,
                Message = "Success"
            });
        }

        // POST: api/Assays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Assay>> PostAssay(AssayDto assayDto)
        {
            var assay = new Assay { 
                Description = assayDto.Description,
                MaximumPrice = assayDto.MaximumPrice,
                MinimumPrice = assayDto.MinimumPrice,
                Name = assayDto.Name,
                Unit = assayDto.Unit,
            };
            _context.Assays.Add(assay);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Data = new { },
                Status = true,
                Message = "Success"
            });
        }

        // DELETE: api/Assays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssay(int id)
        {
            var assay = await _context.Assays.FindAsync(id);
            if (assay == null)
            {
                return NotFound();
            }

            _context.Assays.Remove(assay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AssayExists(int id)
        {
            return _context.Assays.Any(e => e.Id == id);
        }
    }
}
