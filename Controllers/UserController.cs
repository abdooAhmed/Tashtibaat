using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Tashtibaat.Data;
using Tashtibaat.Models;

namespace Tashtibaat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsersController(
            ApplicationDbContext context,
            UserManager<Users> userManager,
            RoleManager<IdentityRole> roleManager,
            IWebHostEnvironment hostEnvironmen
            )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _webHostEnvironment = hostEnvironmen;
        }
        //[HttpGet("Roles")]
        //public async Task<ActionResult<IEnumerable<object>>> GetRoles()
        //{
        //    var roles = _roleManager.Roles.ToList();
        //    return roles;
        //}
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            List<Users> users = await _context.Users.ToListAsync();



            return users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    CreatedAt = u.CreatedAt,
                }).ToList();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);

            return new UserDto {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = user.CreatedAt
            };
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, Users user)
        {
            if (id.ToString() != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(RegisterUserDto data)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Users'  is null.");
            }

            //if (
            //    (Enum.GetNames(typeof(UserRoles)).Length) < data.Role
            //    )
            //{
            //    return BadRequest(new
            //    {
            //        Status = false,
            //        Message = "Please choose a valid role number"
            //    });
            //}

            if (await _userManager.FindByEmailAsync(data.Email) != null)
            {
                return BadRequest(new
                {
                    Status = false,
                    Message = "The user with this Email already exist"
                });
            }

            //User user = data.MakeUserObject();
            //User user = (UserRoles)data.Role switch
            //{
            //    UserRoles.Accountant => new Accountant(),
            //    UserRoles.Cashier => new Cashier(),
            //    UserRoles.Waiter => new Waiter(),
            //    UserRoles.Chef => new Chef(),
            //    UserRoles.Delivery => new Delivery(),
            //    UserRoles.Supplier => new Supplier(),
            //    UserRoles.Customer => new Customer(),
            //    UserRoles.Admin => new Admin(),
            //    _ => new User(),
            //};

            //data.Role = data.Role == 0 ? data.Role + 1 : data.Role;

            //string role = ((UserRoles)data.Role).ToString();
            
            //var roleExist = await _roleManager.RoleExistsAsync(role);
            //if (!roleExist)
            //{
            //    //create the roles and seed them to the database: Question 1
            //    await _roleManager.CreateAsync(new IdentityRole(role));
            //}

            Users user = new()
            {
                UserName = data.Name,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber,
                EmailConfirmed = true
            };
            var createPowerUser = await _userManager.CreateAsync(user, data.Password);
            //if (createPowerUser.Succeeded)
            //{
            //    //here we tie the new user to the role
            //    await _userManager.AddToRoleAsync(user, role);
            //}
            
            //if ((Enum.GetNames(typeof(UserRoles)).Length) -2 >= data.Role)
            //{
            //    QrCodeHelper.GenerateQrCode(user.UserName, DateTime.Now, role, user.Email, _webHostEnvironment);
            //}
            
            return Ok(new 
            { 
                Data = new { id = user.Id },
                Status = true,
                Message = "Success"
            });
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return (_context.Users?.Any(e => e.Id == id.ToString())).GetValueOrDefault();
        }
    }
}
