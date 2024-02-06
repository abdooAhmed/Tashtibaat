using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FoodHub.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tashtibaat.Data;
using Tashtibaat.Models;

namespace Tashtibaat.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<Users> _userManager;
        public ProductsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<Users> userManager)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducts(int id)
        {
            
            
            var products = await _context.Products.Where(x=>x.Category.Id == id).Select(x => new { x.Id, x.Name,x.MinimumPrice,x.MaximumPrice,x.Picture}).ToListAsync();

            if (products == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                Data = products,
                Status = true,
                Message = "Success"
            });
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts(int id, ProductDto dto)
        {
            var products = new Products { Id = dto.Id,MinimumPrice = dto.MinimumPrice,MaximumPrice=dto.MaximumPrice,Name = dto.Name,
                Picture = FileHelper.UploadedFile(dto.Picture, _webHostEnvironment.WebRootPath, "TODO: fix ngrok to get a fixed domain", "Products")
            };
            if (id != products.Id)
            {
                return BadRequest();
            }

            _context.Entry(products).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
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
                Data = new {dto.MinimumPrice,dto.MaximumPrice,dto.Name,products.Picture},
                Status = true,
                Message = "Success"
            });
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> PostProducts(ProductDto productDto)
        {
            var category = await _context.ProductCategories.Where(x => x.Id == productDto.CategoryId).FirstOrDefaultAsync();
            if(category == null)
            {
                return BadRequest(new
                {
                    Data = new
                    {

                    },
                    Status = false,
                    Message = "Please Select a Category"
                });
            }
            try
            {
                var products = new Products
                {
                    Name = productDto.Name,
                    MinimumPrice = productDto.MinimumPrice,
                    MaximumPrice = productDto.MaximumPrice,
                    Picture = FileHelper.UploadedFile(productDto.Picture, _webHostEnvironment.WebRootPath, this.Request.PathBase, "Products"),
                    Category = category
                };
                _context.Products.Add(products);
                await _context.SaveChangesAsync();
            }catch (Exception ex)
            {
                return BadRequest(new
                {
                    Data = new
                    {

                    },
                    Status = false,
                    Message = ex.Message
                });
            }

            return Ok(new
            {
                Data = new
                {
                    
                },
                Status = true,
                Message = "Success"
            });
        }


        [HttpGet("Categories")]
        public async Task<IActionResult> GetProductsCategory()
        {
            var categories = await _context.ProductCategories.Select(x=> new {x.Id,x.Name,x.Picture}).ToListAsync();
            return Ok(new
            {
                Data = categories,
                Status = true,
                Message = "Success"
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Category")]
        public async Task<IActionResult> PostProductsCategory(CategoryDto categoryDto)
        {
            var category = new ProductsCategory 
            {
                Name = categoryDto.Name,
                Picture = FileHelper.UploadedFile(categoryDto.Picture, _webHostEnvironment.WebRootPath, "TODO: fix ngrok to get a fixed domain", "Category")

            };
            _context.ProductCategories.Add(category);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Data = new
                {

                },
                Status = true,
                Message = "Success"
            });
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducts(int id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            _context.Products.Remove(products);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
