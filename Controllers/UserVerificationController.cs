using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Tashtibaat.Models;

namespace Tashtibaat.Controllers
{
    public class UserVerificationController : Controller
    {
        private UserManager<Users> _userManager;
        private SignInManager<Users> _signInManager;
        private RoleManager<IdentityRole> roleManager;

        public UserVerificationController(UserManager<Users> userMngr, SignInManager<Users> signInMngr, RoleManager<IdentityRole> role)
        {
            _userManager = userMngr;
            _signInManager = signInMngr;
            roleManager = role;

        }
        public async Task<IActionResult> VerifyEmail(string UserId, string Code)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null) return BadRequest();
            var result = await _userManager.ConfirmEmailAsync(user, Code);
            if (result.Succeeded)
            {
                return View();
            }
            return BadRequest();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            // If password reset token or email is null, most likely the
            // user tried to tamper the password reset link
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordView model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    // reset the user password
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    // Display validation errors. For example, password reset token already
                    // used to change the password or password complexity rules not met
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist
                return View("ResetPasswordConfirmation");
            }
            // Display validation errors if model state is not valid
            return View(model);
        }
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }

    
}
