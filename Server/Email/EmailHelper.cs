using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Server.Email
{
    public class EmailHelper : IEmailSender
    {
        //public Task SendEmail(string userEmail, string confirmationLink)
        //{
        //    MailMessage mailMessage = new MailMessage();
        //    mailMessage.From = new MailAddress("warpelectricservice@gmail.com");
        //    mailMessage.To.Add(new MailAddress("warpelectricservice@gmail.com"));

        //    mailMessage.Subject = "Confirm your email";
        //    mailMessage.IsBodyHtml = true;
        //    mailMessage.Body = confirmationLink;

        //    SmtpClient client = new SmtpClient();
        //    client.Credentials = new System.Net.NetworkCredential("warpelectricservice@gmail.com", "DotNet123!");
        //    client.Host = "smtpout.secureserver.net";
        //    client.Port = 80;

        //    try
        //    {
        //        client.Send(mailMessage);

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //        // log exception
        //    }

        //}
   
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var emailToSend = new MimeMessage();
                emailToSend.From.Add(MailboxAddress.Parse("warpelectricservice@gmail.com"));
                emailToSend.To.Add(MailboxAddress.Parse("deemclean46@gmail.com"));
                emailToSend.Subject = subject;
                emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };

                using var emailClient = new MailKit.Net.Smtp.SmtpClient();
                emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate("warpelectricservice@gmail.com", "DotNet123!");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new BadHttpRequestException($"Something aint quite right son Error message: {ex.Message}.");
            }

        }

    

    }
}