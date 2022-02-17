﻿using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class License
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FName { get; set; }

        [Required]
        public string LName { get; set; }

        [Required]
        public DateTime dob { get; set; }

        public int YearsHeld { get; set; }



    }
}