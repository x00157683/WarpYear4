using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class EmailDTO
    {




        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
      
}