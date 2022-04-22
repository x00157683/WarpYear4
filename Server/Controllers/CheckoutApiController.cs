using System.Threading.Tasks;
using Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe.BillingPortal;
using Stripe.Checkout;
using Stripe;
using Newtonsoft.Json;

namespace Server.Controllers
{

    [Route("create-checkout-session")]
    [ApiController]
    public class CheckoutApiController : Controller
    {


        [HttpPost]
        public async Task<BookingDTO> Create([FromBody] StripePaymentDTO paymentDTO)
        {

            var content = JsonConvert.SerializeObject(paymentDTO);
            var domain = "https://localhost:7274";
            var options = new Stripe.Checkout.SessionCreateOptions

            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                       Name = "BPENLIGNE",
                                        Description = "Formule Smart",
                                        Amount = 99,
                                        Currency = "eur",
                                        Quantity = 1,
                  },
                },
                Mode = "payment",
                SuccessUrl = domain + "/success.html",
                CancelUrl = domain + "/cancel.html",
            };
            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);


            return new BookingDTO();
        }
    }
}