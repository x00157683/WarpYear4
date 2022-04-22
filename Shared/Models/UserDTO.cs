using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class UserDTO
    {
     
 
     

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

        public DateOnly ?Dob { get; set; }

        [Required]
        public string ?PhoneNumber { get; set; } 

        //public License License { get; set; }

        //public List<Booking> Bookings { get; set; }



    }


    public class RegistrationResponseDto
    {
        public bool IsSuccessfulRegistration { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}