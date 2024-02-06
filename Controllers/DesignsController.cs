using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tashtibaat.Data;
using Tashtibaat.Models;

namespace Tashtibaat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DesignsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Designs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Designs>>> GetDesigns()
        {
            var designs = await _context.Designs.Select(x => new { x.Name, x.Id, x.MinimumPrice, x.MaximumPrice, x.Description, x.Unit }).ToListAsync();
            return Ok(new
            {
                Data = designs,
                Status = true,
                Message = "Success"
            });
        }

        // GET: api/Designs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Designs>> GetDesigns(int id)
        {
            var designs = await _context.Designs.FindAsync(id);

            if (designs == null)
            {
                return NotFound();
            }

            return designs;
        }

        // PUT: api/Designs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDesigns(int id, DesignsDto designsDto)
        {
            var designs = new Designs
            {
                Id = id,
                Description = designsDto.Description,
                MaximumPrice = designsDto.MaximumPrice,
                MinimumPrice = designsDto.MinimumPrice,
                Name = designsDto.Name,
                Unit = designsDto.Unit,
            };
            if (id != designs.Id)
            {
                return BadRequest();
            }

            _context.Entry(designs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DesignsExists(id))
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

        // POST: api/Designs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Designs>> PostDesigns(DesignsDto designsDto)
        {
            var designs = new Designs
            {
                Description = designsDto.Description,
                MaximumPrice = designsDto.MaximumPrice,
                MinimumPrice = designsDto.MinimumPrice,
                Name = designsDto.Name,
                Unit = designsDto.Unit,
            };
            _context.Designs.Add(designs);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Data = new { },
                Status = true,
                Message = "Success"
            });
        }

        // DELETE: api/Designs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDesigns(int id)
        {
            var designs = await _context.Designs.FindAsync(id);
            if (designs == null)
            {
                return NotFound();
            }

            _context.Designs.Remove(designs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DesignsExists(int id)
        {
            return _context.Designs.Any(e => e.Id == id);
        }
    }
}
