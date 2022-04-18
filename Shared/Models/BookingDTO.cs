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
        public string BookingDTOId { get; set; } //test
       
        public DateTime StartTime { get; set; }

        public DateTime ?StopTime { get; set; }

        public double Cost { get; set; }

        public string Location { get; set; }

        public bool IsCreated { get; set; }

        public bool IsComplete { get; set; }


        public int CarId { get; set; }

        //public Car Car { get; set; }

        public string? UserEmail { get; set; }








    }
}
