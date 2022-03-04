using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class CarDTO
    {

        [Key]
        public int CarDTOId { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public double Range { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public double PricePerUnit { get; set; }
        [Required]
        public bool isLocked { get; set; }
       
        public double RangeLeft { get; set; }
        
        public bool Active { get; set; }



    }
}
