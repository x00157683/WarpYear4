
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Shared.Models;

namespace Server.Services
{
    public interface IPaymentService
    {

        public Task<SuccessModelDTO>Checkout(StripePaymentDTO model);
        


    }
}
