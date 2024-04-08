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
            var currentPic = await _context.Products.Where(x => x.Id == id).Select(x=>x.Picture).FirstOrDefaultAsync();
            var products = new Products { Id = id,MinimumPrice = dto.MinimumPrice,MaximumPrice=dto.MaximumPrice,Name = dto.Name,
                Picture = dto.Picture != null ? FileHelper.UploadedFile(dto.Picture, _webHostEnvironment.WebRootPath, "TODO: fix ngrok to get a fixed domain", "Products"): currentPic
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
                Data = new {products.Id,dto.MinimumPrice,dto.MaximumPrice,dto.Name,products.Picture},
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
                return Ok(new
                {
                    Data = new
                    {
                        products.Id,
                        productDto.Name,
                        productDto.MinimumPrice,
                        productDto.MaximumPrice,
                        products.Picture,

                    },
                    Status = true,
                    Message = "Success"
                });
            }
            catch (Exception ex)
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

        }


        [HttpGet("Categories")]
        public async Task<IActionResult> GetProductsCategory()
        {
            var categories = await _context.ProductCategories.Select(x=> new {x.Id,x.Name,x.Picture,Products = x.Products.Select(p => p.Name).Take(3) }).ToListAsync();
            return Ok(new
            {
                Data = categories,
                Status = true,
                Message = "Success"
            });
        }

        [Authorize(Roles = "Admin")]
        // DELETE: api/Products/5
        [HttpDelete("Category/{id}")]
        public async Task<IActionResult> DeleteProductsCategory(int id)
        {
            var products = await _context.ProductCategories.FindAsync(id);
            if (products == null)
            {
                return NotFound(new
                {

                    Data = new
                    {

                    },
                    Status = false,
                    Message = "Category not found"
                });
            }

            _context.ProductCategories.Remove(products);
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

        [Authorize(Roles = "Admin")]
        [HttpPut("Category/{id}")]
        public async Task<IActionResult> PutProductsCategory(int id, CategoryDto categoryDto)
        {
            var currentPic = await _context.ProductCategories.Where(x=>x.Id== id).Select(x=>x.Picture).FirstOrDefaultAsync();
            var category = new ProductsCategory
            {
                Id = id,
                Name = categoryDto.Name,
                Picture = categoryDto.Picture != null ? FileHelper.UploadedFile(categoryDto.Picture, _webHostEnvironment.WebRootPath, "TODO: fix ngrok to get a fixed domain", "Category") : currentPic

            };
            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

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
                Data = new {category.Id, categoryDto.Name, category.Picture },
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
                Data = new { category.Name,category.Picture,category.Id},
                Status = true,
                Message = "Success"
            });
        }

        [Authorize(Roles = "Admin")]
        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducts(int id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound(new
                {

                    Data = new
                    {

                    },
                    Status = false,
                    Message = "Product not found"
                });
            }

            _context.Products.Remove(products);
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

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
