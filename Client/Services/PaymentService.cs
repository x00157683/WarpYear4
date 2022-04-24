﻿
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using Shared.Models;
using System.Text;

namespace Server.Services
{
    public class PaymentService : IPaymentService
    {
        
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
       
        }

        public async Task<SuccessModelDTO> Checkout(StripePaymentDTO model)
        {

            try
            {
                var content = JsonConvert.SerializeObject(model);
                var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://localhost:5003/api/stripepayment/create", bodyContent);
                string responseResult = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<SuccessModelDTO>(responseResult);
                    result.IsPaid = true;
                    return result;
                }
                else
                {

                    throw new Exception(responseResult.ToString());
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

            
    }
}
