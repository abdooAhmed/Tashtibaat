using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Tashtibaat.Data;
using Tashtibaat.Helpers;
using Tashtibaat.Models;

namespace Tashtibaat.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Users> _userManager;
        private readonly ISendMail _sendMail;

        public OrdersController(ApplicationDbContext context, UserManager<Users> userManager, ISendMail sendMail)
        {
            _context = context;
            _userManager = userManager;
            _sendMail = sendMail;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Product")]
        public async Task<ActionResult<Order>> PostOrder(OrderDto dto)
        {
            
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByNameAsync(userName);
            List<ProductToMeters> ProductToMeters = new List<ProductToMeters>();
            foreach (var i in dto.ProductToMeters)
            {
                var product = await _context.Products.Where(x => x.Id == i.ProductId).FirstOrDefaultAsync();
                if(product == null)
                {
                    return BadRequest(new
                    {
                        Data = new { },
                        Status = false,
                        Message = "عليك اختيار منتج"
                    });
                }
                ProductToMeters.Add(new ProductToMeters { Products = product, Number = i.Metters });
            }
            var order = new Order
            {
                Total = dto.Total,
                Quantity = dto.Quantity,
                Bank = dto.Bank,
                Cash = dto.Cash,
                Address = dto.Address,
                Notes = dto.Notes,
                Users = user,
                ProductToMeters = ProductToMeters,

            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
             _sendMail.Send(user, "طلب شراء منتجات", "");
            return Ok(new
            {
                Data = new { },
                Status = true,
                Message = "سوف نرد عليك في اقرب وقت"
            });
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        [HttpPost("Assay")]
        public async Task<ActionResult> AddAssayOrder(AssayOrderDto dto)
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByNameAsync(userName);
            List<AssayToMeters> AssayToMeters = new List<AssayToMeters>();
            foreach(var i in dto.AssayToMeters)
            {
                var assay = await _context.Assays.Where(x => x.Id == i.AssayId).FirstOrDefaultAsync();
                if (assay == null)
                {
                    return BadRequest(new
                    {
                        Data = new { },
                        Status = false,
                        Message = "عليك اختيار منتج"
                    });
                }
                AssayToMeters.Add(new AssayToMeters { Assay = assay, Number = i.Metters });
            }
            var assayOrder = new AssayOrder
            {
                Address = dto.Address,
                Bank = dto.Bank,
                Cash = dto.Cash,
                Notes = dto.Notes,
                Total = dto.Total,
                User = user,
                AssayToMeters = AssayToMeters,
            };
            await _context.AssayOrders.AddAsync(assayOrder);
            await _context.SaveChangesAsync();
            var status = _sendMail.Send(user, "طلب مقايسه", "");
            if (status)
            {
                return Ok(new
                {
                    Data = new { },
                    Status = true,
                    Message = "سوف نرد عليك في اقرب وقت"
                });
            }
            else
            {
                return BadRequest(new
                {
                    Data = new { },
                    Status = true,
                    Message = "please try again later"
                });
            }
        }

        [HttpPost("Desgin")]
        public async Task<ActionResult> AddDesginOrder(DesignOrderDto dto)
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByNameAsync(userName);
            List<DesignToMeters> DesignToMeters = new List<DesignToMeters>();
            foreach (var i in dto.DesignToMeters)
            {
                var design = await _context.Designs.Where(x => x.Id == i.DesignId).FirstOrDefaultAsync();
                if (design == null)
                    return BadRequest(new
                    {
                        Data = new { },
                        Status = false,
                        Message = "عليك اختيار منتج"
                    });
                DesignToMeters.Add(new DesignToMeters { Designs = design, Number = i.Metters });
            }
            var designOrder = new DesignOrder
            {
                Address = dto.Address,
                Bank = dto.Bank,
                Cash = dto.Cash,
                Notes = dto.Notes,
                Total = dto.Total,
                User = user,
                DesignToMeters = DesignToMeters,
            };
            await _context.DesignOrders.AddAsync(designOrder);
            await _context.SaveChangesAsync();
            var status = _sendMail.Send(user, "طلب تصميم", "");
            if (status)
            {
                return Ok(new
                {
                    Data = new { },
                    Status = true,
                    Message = "سوف نرد عليك في اقرب وقت"
                });
            }
            else
            {
                return BadRequest(new
                {
                    Data = new { },
                    Status = true,
                    Message = "please try again later"
                });
            }
        }


        [HttpPost("Kitchen")]
        public async Task<ActionResult> AddKitchenOrder(KitchenAndDressingRoomOrderDto dto)
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByNameAsync(userName);
            if (ModelState.IsValid)
            {
                ServicesOrder servicesOrder = new ServicesOrder 
                {
                    Address = dto.Address,
                    Notes = dto.Notes,
                    PhoneNubmer = dto.PhoneNubmer,
                    User = user
                };
                try
                {
                    await _context.ServicesOrders.AddAsync(servicesOrder);
                    var kitchen = await _context.Kitchens.Where(x => x.Id == dto.ServiceId).FirstAsync();
                    KitchenToOrder kitchenToOrder = new KitchenToOrder
                    {
                        Kitchen = kitchen,
                        Space = dto.Space,
                        ServicesOrder = servicesOrder
                    };
                    await _context.KitchenToOrders.AddAsync(kitchenToOrder);
                    await _context.SaveChangesAsync();
                }catch (Exception ex)
                {
                    return BadRequest(new
                    {
                        Data = new { },
                        Status = false,
                        Message = ex.Message
                    });
                }

                var status = _sendMail.Send(user, "طلب مطبخ", "");
                if (status)
                {
                    return Ok(new
                    {
                        Data = new { },
                        Status = true,
                        Message = "سوف نرد عليك في اقرب وقت"
                    });
                }
            }
            return BadRequest(new
            {
                Data = new { },
                Status = false,
                Message = "Please fill all required fields"
            });
        }

        [HttpPost("DressingRoom")]
        public async Task<ActionResult> AddDressingRoomOrder(KitchenAndDressingRoomOrderDto dto)
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByNameAsync(userName);
            if (ModelState.IsValid)
            {
                ServicesOrder servicesOrder = new ServicesOrder
                {
                    Address = dto.Address,
                    Notes = dto.Notes,
                    PhoneNubmer = dto.PhoneNubmer,
                    User = user
                };
                try
                {
                    await _context.ServicesOrders.AddAsync(servicesOrder);
                    var dressingRoom = await _context.DressingRooms.Where(x => x.Id == dto.ServiceId).FirstAsync();
                    DressingRoomToOrder dressingRoomToOrder = new DressingRoomToOrder
                    {
                        DressingRoom = dressingRoom,
                        Space = dto.Space,
                        ServicesOrder = servicesOrder
                    };
                    await _context.DressingRoomToOrders.AddAsync(dressingRoomToOrder);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return BadRequest(new
                    {
                        Data = new { },
                        Status = false,
                        Message = ex.Message
                    });
                }
                var status = _sendMail.Send(user, "طلب غرفة ملابس", "");
                if (status)
                {
                    return Ok(new
                    {
                        Data = new { },
                        Status = true,
                        Message = "سوف نرد عليك في اقرب وقت"
                    });
                }
            }
            return BadRequest(new
            {
                Data = new { },
                Status = false,
                Message = "Please fill all required fields"
            });
        }

        [HttpPost("ArmoredDoor")]

        public async Task<ActionResult> AddArmoredDoorOrder(ServicesOrderDto dto)

        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByNameAsync(userName);
            if (ModelState.IsValid)
            {
                ServicesOrder servicesOrder = new ServicesOrder
                {
                    Address = dto.Address,
                    Notes = dto.Notes,
                    PhoneNubmer = dto.PhoneNubmer,
                    User = user
                };
                try
                {
                    await _context.ServicesOrders.AddAsync(servicesOrder);
                    var armoredDoor = await _context.ArmoredDoors.Where(x => x.Id == dto.ServiceId).FirstAsync();
                    ArmoredDoorTOOrder armoredDoorTOOrder = new ArmoredDoorTOOrder
                    {
                        ArmoredDoor = armoredDoor,
                        ServicesOrder = servicesOrder
                    };
                    await _context.ArmoredDoorTOOrders.AddAsync(armoredDoorTOOrder);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return BadRequest(new
                    {
                        Data = new { },
                        Status = false,
                        Message = ex.Message
                    });
                }
                var status = _sendMail.Send(user, "طلب ابواب مصفحه", "");
                if (status)
                {
                    return Ok(new
                    {
                        Data = new { },
                        Status = true,
                        Message = "سوف نرد عليك في اقرب وقت"
                    });
                }
            }
            return BadRequest(new
            {
                Data = new { },
                Status = false,
                Message = "Please fill all required fields"
            });
        }

        [HttpPost("SecuritySystem")]

        public async Task<ActionResult> AddSecuritySystemOrder(ServicesOrderDto dto)

        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByNameAsync(userName);
            if (ModelState.IsValid)
            {
                ServicesOrder servicesOrder = new ServicesOrder
                {
                    Address = dto.Address,
                    Notes = dto.Notes,
                    PhoneNubmer = dto.PhoneNubmer,
                    User = user
                };
                try
                {
                    await _context.ServicesOrders.AddAsync(servicesOrder);
                    var securitySurveillance = await _context.SecuritySurveillances.Where(x => x.Id == dto.ServiceId).FirstAsync();
                    SecuritySystemToOrder securitySystemToOrder = new SecuritySystemToOrder
                    {
                        SecuritySurveillance = securitySurveillance,
                        ServicesOrder = servicesOrder
                    };
                    await _context.SecuritySystemToOrders.AddAsync(securitySystemToOrder);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return BadRequest(new
                    {
                        Data = new { },
                        Status = false,
                        Message = ex.Message
                    });
                }
                var status = _sendMail.Send(user, "طلب انظمة امن ومراقبه", "");
                if (status)
                {
                    return Ok(new
                    {
                        Data = new { },
                        Status = true,
                        Message = "سوف نرد عليك في اقرب وقت"
                    });
                }
            }
            return BadRequest(new
            {
                Data = new { },
                Status = false,
                Message = "Please fill all required fields"
            });
        }
    }
}
