using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shared.Models
{
    public class AppUser : IdentityUser
    {

      
        [NotMapped]
        public List<Booking> Bookings { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(80, ErrorMessage = "Your password must be between {2} and {1} characters.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string Name { get; set; }

        public string ConfirmPassword { get; set; }

        public DateOnly? Dob { get; set; }

       
        public string PhoneNumber { get; set; }

        public double Balance { get; set; }

        [NotMapped]
        public override bool EmailConfirmed { get; set; }
        [NotMapped]
    
        public override bool LockoutEnabled { get; set; }
        [NotMapped]
        public override int AccessFailedCount { get; set; }
  
        [NotMapped]
        public override string? ConcurrencyStamp { get; set; }
        [NotMapped]
        public override string? SecurityStamp { get; set; }
        [NotMapped]
        public override DateTimeOffset? LockoutEnd { get; set; }
        [NotMapped]
        public override bool TwoFactorEnabled { get; set; }
        [NotMapped]
        public override bool PhoneNumberConfirmed { get; set; }
        public void Update(AppUser user)
        {
            UserName = user.UserName;
            Email = user.Email;
            PasswordHash = user.PasswordHash;
        }
        public void Update(string userName, string? organization,
            string email, string Password)
        {
            UserName = userName;
            Email = email;
            // set hashed password text to PasswordHash.
            PasswordHash = new PasswordHasher<AppUser>()
                .HashPassword(this, Password);
        }
        public string Validate()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return "UserName is required";
            }
            if (string.IsNullOrEmpty(Email))
            {
                return "E-Mail address is required";
            }
            if (string.IsNullOrEmpty(PasswordHash))
            {
                return "Password is required";
            }
            return "";
        }

    }
}
