using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tashtibaat.Models;
using Tashtibaat.Data;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Tashtibaat.Authorization;
using Tashtibaat.Helpers;
using MimeKit;
using System.Security.Authentication;
using MailKit.Net.Smtp;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tashtibaat.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ISendMail _sendMail;
        //public SendMail sendMail;

        public AuthController(
            
            SignInManager<Users> signInManager,
            UserManager<Users> userManager,
            ApplicationDbContext dbContext,
            IConfiguration configuration,
            RoleManager<IdentityRole> roleManager,
            IWebHostEnvironment hostEnvironmen,
            ISendMail sendMail
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = dbContext;
            _configuration = configuration;
            _roleManager = roleManager;
            _webHostEnvironment = hostEnvironmen;
            _sendMail = sendMail;
        }


        private async Task<IdentityResult> EnsureRole(string uid, string role)
        {
            

            if (_roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await _roleManager.RoleExistsAsync(role))
            {
                IR = await _roleManager.CreateAsync(new IdentityRole(role));
            }

            

            //if (userManager == null)
            //{
            //    throw new Exception("userManager is null");
            //}

            var user = await _userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await _userManager.AddToRoleAsync(user, role);

            return IR;
        }

        private async Task<bool> CheckUserConfirmation(Users user)
        {
            return await _userManager.IsEmailConfirmedAsync(user);
        }

        [HttpPost("registration")]
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
                    Data = new {},
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
                EmailConfirmed = false
            };
            var createPowerUser = await _userManager.CreateAsync(user, data.Password);
            //if (createPowerUser.Succeeded)
            //{
            //    //here we tie the new user to the role
            //    await _userManager.AddToRoleAsync(user, role);
            //}

            //if ((Enum.GetNames(typeof(UserRoles)).Length) - 2 >= data.Role)
            //{
            //    QrCodeHelper.GenerateQrCode(user.UserName, DateTime.Now, role, user.Email, _webHostEnvironment);
            //}

            if (createPowerUser.Succeeded)
            {
                await EnsureRole(user.Id, Constants.Client);
                var succeeded = await SendMail(user, "registration");

                if(succeeded)
                {
                    return Ok(new
                    {
                        Data = new { id = user.Id },
                        Status = true,
                        Message = "please check your mailbox to confirm your account"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Data = new { },
                        Status = false,
                        Message = "Pleas try again"
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    Data = new { },
                    Status = false,
                    Message = createPowerUser.Errors.FirstOrDefault().Description
                });
            }
        }

        

        //[HttpGet]
        //public  ActionResult<IEnumerable<Object>> GetAllRoles()
        //{
        //    var code = (int)(Enum.Parse(typeof(UserRoles), "Visitor"));
        //    var roles = _roleManager.Roles.Where(x => x.Name == "Client" || x.Name == "Visitor").Select(x=> new
        //    {

        //        role = x.Name
        //    }).ToList();
        //    var data = new List<Object>();
        //    foreach(var x in roles)
        //    {
        //        data.Add(new { });
        //    }
        //    return roles;
        //}

        // Login Api + methods
        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<IActionResult> Token(TokenRequestDTO tokenRequest)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(tokenRequest.Email);
                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, tokenRequest.Password, false);
                    if (result.Succeeded)
                    {
                        string token = await JwtGeneration(user.Email);
                        string refresh = RefreshTokenGeneration();
                        user.RefreshToken = refresh;
                        //TODO: case: RefreshTokenExpireDate
                        //user.CreatedDate = DateTime.UtcNow.AddDays(7); 
                        await _userManager.UpdateAsync(user);
                        var Role = await _userManager.GetRolesAsync(user);
                        
                        return Ok(new
                        {   
                            Data = new {
                                Token = token,
                                Status = "Authenticated",
                                RefreshToken = refresh,
                                IsAdmin= (Role.FirstOrDefault() == "Admin" ? true : false) 
                            },
                            Status = true,
                            Message = "Success"
                        });
                    }
                    else
                    {
                        var status = await CheckUserConfirmation(user);

                        if (!status)
                        {
                            status = await SendMail(user, "registration");
                            if (status)
                            {
                                return BadRequest(new FailedRequest
                                {
                                    Data = null,
                                    Status = false,
                                    Message = "please check your mailbox to confirm your account"
                                });
                            }
                        }
                        return BadRequest(new FailedRequest
                        {
                            Data = null,
                            Status = false,
                            Message = "The password that you've entered is incorrect."
                        });
                    }
                }
                else
                {
                    return BadRequest(new FailedRequest
                    {
                        Data = null,
                        Status = false,
                        Message = "The email address you entered isn't connected to an account."
                    });
                }
            }
            else
            {
                return BadRequest(new FailedRequest
                {
                    Data = null,
                    Status = false,
                    Message = ModelState.ErrorCount.ToString()
                });
            }

        }


        // Refresh token Api + methods
        [AllowAnonymous]
        [HttpPost("token/refresh")]
        public async Task<IActionResult> RefreshToken(RefreshTokenReqDTO RefreshModel)
        {
            try
            {
                var user = _context.Users.Where(
                    U => U.RefreshToken == RefreshModel.RefreshToken
                    ).FirstOrDefault();

                // TODO: IF we want to validate refresh token expiry date ->
                // add this(|| user.CreatedDate < DateTime.UtcNow) + remove comment
                // from case: RefreshTokenExpireDate
                if ((user == null))
                {
                    return BadRequest(new 
                    {
                        Data = new {},
                        Status = false,
                        Message = "not found"
                    });
                }
                else
                {
                    var output = GetInfoJwtToken(RefreshModel.Token);
                    var token = GenerateAccessToken(output.Claims);
                    var refreshToken = RefreshTokenGeneration();
                    user.RefreshToken = refreshToken;
                    await _userManager.UpdateAsync(user);

                    return Ok(new 
                    {
                       Data = new
                       {
                           Token = token,
                           Status = "Authenticated",
                           RefreshToken = refreshToken,
                       },
                        Status = true,
                        Message = "Success"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new 
                {
                    Data = new {},
                    Status = false,
                    Message = ex.Message
                });
            }

        }


        private async Task<string> JwtGeneration(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Iss, _configuration["Jwt:Issuer"].ToString()),
                    new Claim(JwtRegisteredClaimNames.Aud, _configuration["Jwt:Audience"].ToString()),
                    new Claim("uid", user.Id)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = signingCredentials
            };

            tokenDes.Subject.AddClaims(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));
            var token = jwtTokenHandler.CreateToken(tokenDes);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }

        [HttpPost("forgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            // If the user is found AND Email is confirmed
            if (user != null && await _userManager.IsEmailConfirmedAsync(user))
            {
                var succeeded = await SendMail(user, "ResetPassword");
                if (succeeded)
                {
                    return Ok(new
                    {
                        Data = new { },
                        Status = true,
                        Message = "Please look in your mailbox"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Data = new { },
                        Status = false,
                        Message = "Please try again later"
                    });
                }
            }
            var status = await CheckUserConfirmation(user);
            if (!status)
            {
                await SendMail(user, "registration");
                return BadRequest(new
                {
                    Data = new { },
                    Status = false,
                    Message = "please check your mailbox to confirm your account"
                });
            }
            return BadRequest(new
            {
                Data = new { },
                Status = false,
                Message = "The email address you entered isn't connected to an account."
            });
        }

        private string RefreshTokenGeneration()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal GetInfoJwtToken(string JwtToken)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricSecurityKey,
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(JwtToken, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var InputClaims = new ClaimsIdentity();
            foreach (var claim in claims)
            {
                InputClaims.AddClaim(claim);
            }

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = InputClaims,
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = signinCredentials

            };
            var token = jwtTokenHandler.CreateToken(tokenDes);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }

        public async Task<bool> SendMail(Users user,string For)
        {
            if (For == "registration") 
            {
                var Code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var Link = Url.Action("VerifyEmail", "UserVerification", new { UserId = user.Id, Code }, Request.Scheme);
                var status = _sendMail.Send(user.UserName, user.Email, Link, "Email Confirmation");
                return status;
            }
            else if(For == "ResetPassword")
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResetLink = Url.Action("ResetPassword", "UserVerification",
                        new { email = user.Email, token = token }, Request.Scheme);
                var status = _sendMail.Send(user.UserName, user.Email, passwordResetLink, "Reset Your Password");
                return status;
            }
            return false;
        }
    }

    public class TokenRequestDTO
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }

    public class ForgetPasswordDto
    {
        [Required]
        public string Email { get; set; } = null!;
    }

    public class TokenResponse
    {
        public string? Token { get; set; }
        public string Status { get; set; } = null!;
        public string? RefreshToken { get; set; }
    }

    public class FailedRequest
    {
        public Object Data { get;set; }
        public bool Status { get; set; }
        public string Message { get;set; }
    }

    public class RefreshTokenReqDTO
    {
        [Required]
        public string Token { get; set; } = null!;
        [Required]
        public string RefreshToken { get; set; } = null!;
    }
}
