using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class SuccessModelDTO
    {

        public BookingDTO Booking { get; set; }

        public double Price { get; set; }

        public string Data { get; set; }

        public bool IsPaid { get; set; }

    }
}
