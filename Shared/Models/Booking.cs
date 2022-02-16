using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Booking
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime TimeRemain { get; set; }

        [Required]
        public DateTime TimeElapsed { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        public Car Cars { get; set; }


    }
}
