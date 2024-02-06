using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Tashtibaat.Models
{
    public class Users : IdentityUser
    {
        [Required]
        public string Name { get; set; } = String.Empty;

        // for the mobile (or web) JWT users
        public string? RefreshToken { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        // TODO: Use AspNetRoles instead?
        //[Required]
        //public UserType Type { get; set; }

        //public Account AccountId { get; set; }

        //public List<Notification> Notifications { get; set; }
    }

    public class UserDto
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

    }


    public class RegisterUserDto
    {

        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string ConfirmPassword { get; set; } = null!;

        //[Required]
        //public uint Role { get; set; }
        //public ICollection<string> Roles { get; set; } = null!;


        //public User? MakeUserObject()
        //{
        //    return (UserRoles)this.Role switch
        //    {
        //        UserRoles.Admin => new Admin(),
        //        UserRoles.Accountant => new Accountant(),
        //        UserRoles.Cashier => new Cashier(),
        //        UserRoles.Waiter => new Waiter(),
        //        UserRoles.Chef => new Chef(),
        //        UserRoles.Delivery => new Delivery(),
        //        UserRoles.Supplier => new Supplier(),
        //        UserRoles.Customer => new Customer(),
        //        _ => new User(),
        //    };
        //}
        //public static User GetUserType(UserRoles role)
        //{
        //    return role switch
        //    {
        //        UserRoles.Admin => new Admin(),
        //        UserRoles.Accountant => new Accountant(),
        //        UserRoles.Cashier => new Cashier(),
        //        UserRoles.Waiter => new Waiter(),
        //        UserRoles.Chef => new Chef(),
        //        UserRoles.Delivery => new Delivery(),
        //        UserRoles.Supplier => new Supplier(),
        //        UserRoles.Customer => new Customer(),
        //        _ => new User(),
        //    };
        //}
    }
    public class ResetPasswordView
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }

}
