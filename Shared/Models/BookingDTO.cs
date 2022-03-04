using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class BookingDTO
    {

        [Key]
        public int BookingDTOId { get; set; } //test
        [Required]
        public string StartTime { get; set; }

        public string ?StopTime { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        public bool IsCreated { get; set; }

        [Required]
        public bool IsComplete { get; set; }

        //public int CarId { get; set; }

        //public Car Car { get; set; }

        //public int UserId { get; set; }

        //public User User { get; set; }  






    }
}
