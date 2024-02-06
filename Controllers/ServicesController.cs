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
    public class ServicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Users> _userManager;
        private readonly ISendMail _sendMail;

        public ServicesController(ApplicationDbContext context, UserManager<Users> userManager, ISendMail sendMail)
        {
            _context = context;
            _userManager = userManager;
            _sendMail = sendMail;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("ArmoredDoor")]
        public async Task<ActionResult> PostArmoredDoor(ArmoredDoor armoredDoor)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _context.ArmoredDoors.AddAsync(armoredDoor);
                    await _context.SaveChangesAsync();
                    return Ok(new
                    {
                        Data = new { },
                        Status = true,
                        Message = "Success"
                    });
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
            }
            return BadRequest(new
            {
                Data = new { },
                Status = false,
                Message = "Please add a correct data"
            });
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("Kitchen")]
        public async Task<ActionResult> PostKitchen(Kitchen kitchen)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Kitchens.AddAsync(kitchen);
                    await _context.SaveChangesAsync();
                    return Ok(new
                    {
                        Data = new { },
                        Status = true,
                        Message = "Success"
                    });
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
            }
            return BadRequest(new
            {
                Data = new { },
                Status = false,
                Message = "Please add a correct data"
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("DressingRoom")]
        public async Task<ActionResult> PostDressingRoom(DressingRoom dressingRoom)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.DressingRooms.AddAsync(dressingRoom);
                    await _context.SaveChangesAsync();
                    return Ok(new
                    {
                        Data = new { },
                        Status = true,
                        Message = "Success"
                    });
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
            }
            return BadRequest(new
            {
                Data = new { },
                Status = false,
                Message = "Please add a correct data"
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Security")]
        public async Task<ActionResult> PostSecuritySurveillance(SecuritySurveillance securitySurveillance)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.SecuritySurveillances.AddAsync(securitySurveillance);
                    await _context.SaveChangesAsync();
                    return Ok(new
                    {
                        Data = new { },
                        Status = true,
                        Message = "Success"
                    });
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
            }
            return BadRequest(new
            {
                Data = new { },
                Status = false,
                Message = "Please add a correct data"
            });
        }

        [HttpGet("Security")]
        public async Task<ActionResult> GetSecuritySurveillance()
        {
            return Ok(new
            {
                Data = await _context.SecuritySurveillances.Select(x => new {x.MinimumPrice,x.MaximumPrice}).ToListAsync(),
                Status = true,
                Message = "Success"
            });
        }

        [HttpGet("DressingRoom")]
        public async Task<ActionResult> GetDressingRoom()
        {
            return Ok(new
            {
                Data = await _context.DressingRooms.Select(x => new { x.MinimumPrice, x.MaximumPrice }).ToListAsync(),
                Status = true,
                Message = "Success"
            });
        }


        [HttpGet("Kitchen")]
        public async Task<ActionResult> GetKitchen()
        {
            return Ok(new
            {
                Data = await _context.Kitchens.Select(x => new { x.MinimumPrice, x.MaximumPrice }).ToListAsync(),
                Status = true,
                Message = "Success"
            });
        }

        [HttpGet("ArmoredDoor")]
        public async Task<ActionResult> GetArmoredDoor()
        {
            return Ok(new
            {
                Data = await _context.ArmoredDoors.Select(x => new { x.MinimumPrice, x.MaximumPrice }).ToListAsync(),
                Status = true,
                Message = "Success"
            });
        }
    }
}
