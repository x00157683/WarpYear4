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
        public string BookingId { get; set; } //test
     
        public string StartTime { get; set; }

        public string ?StopTime { get; set; }

        public string Location { get; set; }

        public double Cost { get; set; }

        public bool IsCreated { get; set; }


        public bool IsComplete { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; }

        public string UserEmail { get; set; }


    }
}
