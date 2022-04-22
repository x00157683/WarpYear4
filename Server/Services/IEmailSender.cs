//using MailKit.Net.Smtp;
//using Microsoft.AspNetCore.Identity.UI.Services;
//using Microsoft.Extensions.Options;
//using MimeKit;
//using SendGrid;
//using SendGrid.Helpers.Mail;

//namespace Server.Services
//{
//    public class EmailSender : IEmailSender
//    {
//        private readonly ILogger _logger;

//        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
//                           ILogger<EmailSender> logger,IConfiguration configuration)
//        {
//            Options = optionsAccessor.Value;
//            _logger = logger;
//            Configuration = configuration;
//        }

//        public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.

//        public IConfiguration Configuration { get; }

//        public async Task SendEmailAsync(string toEmail, string subject, string message)
//        {
//            //if (string.IsNullOrEmpty(Options.SendGridKey))
//            //{
//            //    Options.SendGridKey = Configuration["SendGrid:APIKey"];
//            //}

//            //if (string.IsNullOrEmpty(Options.SendGridKey))
//            //{
//            //    Options.SendGridKey = Configuration["AppSettings:AppUser"];
//            //}

//            //await Execute(Options.SendGridKey, subject, message, toEmail);
//            try
//            {
//                var emailToSend = new MimeMessage();
//                emailToSend.From.Add(MailboxAddress.Parse("FuckYouMark@Storage.com"));
//                emailToSend.To.Add(MailboxAddress.Parse("deemclean46@gmail.com"));
//                emailToSend.Subject = subject;
//                emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };

//                using var emailClient = new SmtpClient();
//                emailClient.Connect("smtp@gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
//                emailClient.Authenticate("warpelectricservice@gmail.com", "DotNet123!");
//                emailClient.Send(emailToSend);
//                emailClient.Disconnect(true);
//            }

//            catch(Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                throw ex;
//            }
//        }

//        public async Task Execute(string apiKey, string subject, string message, string toEmail)
//        {
//            var client = new SendGridClient(apiKey);
//            var sender = Configuration["AppSettings:AppEmail"];
//            var msg = new SendGridMessage()
//            {
//                From = new EmailAddress(sender, "Password Recovery"),
//                Subject = subject,
//                PlainTextContent = message,
//                HtmlContent = message
//            };
//            msg.AddTo(new EmailAddress(toEmail));

//            // Disable click tracking.
//            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
//            msg.SetClickTracking(false, false);
//            var response = await client.SendEmailAsync(msg);
//            _logger.LogInformation(response.IsSuccessStatusCode
//                                   ? $"Email to {toEmail} queued successfully!"
//                                   : $"Failure Email to {toEmail}");
//        }
//    }
//    }
