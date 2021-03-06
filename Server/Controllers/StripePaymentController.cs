using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Stripe.Checkout;

namespace Server.Controllers
{

    [Route("api/[controller]/[action]")]
    public class StripePaymentController : Controller
    {

        [HttpPost]
        [ActionName("create")]
        //[Authorize]
        public async Task<IActionResult> Create([FromBody] StripePaymentDTO paymentModel)
        {
            try
            {
                var domain = "https://victorious-desert-01a818203.1.azurestaticapps.net/";
                var options = new SessionCreateOptions

                {
                    Mode = "payment",
                    SuccessUrl = domain + $"/receipt/{ paymentModel.Booking.BookingDTOId }",
                    CancelUrl = domain + "/",
                    PaymentMethodTypes = new List<string> { "card" },


                    LineItems = new List<SessionLineItemOptions>
                    {

                      
                      new SessionLineItemOptions
                      {
                          Quantity = 1,
                          Description = "Account Credit",
                          PriceData = new SessionLineItemPriceDataOptions
                          {
                              
                              ProductData = new SessionLineItemPriceDataProductDataOptions
                              { 
                                  Name = paymentModel.Name,
                              },
                              UnitAmount = (long?)(paymentModel.Price*100),//20.00 = 2000
                              Currency = "eur",
                              //Product = new SessionLineItemPriceDataProductDataOptions
                              //{
                              //    Name = paymentModel.Name,
                              //}
                              
                          }
                          

                      },
                    },

                };
                var service = new SessionService();
                Session session = service.Create(options);

                Response.Headers.Add("Location", session.Url);





                return Ok(new SuccessModelDTO() { Data = session.Id });
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
