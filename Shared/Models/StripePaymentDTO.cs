using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class StripePaymentDTO
    {

        public BookingDTO Booking { get; set; }

        public double Price { get; set; }

        public string Name { get; set; }



    }
}
