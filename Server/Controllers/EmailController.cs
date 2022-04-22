using System.Threading.Tasks;
using Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

namespace Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : Controller
    {
        private UserManager<AppUser> userManager;
        private readonly AppDBContext _appDBContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        public EmailController(AppDBContext appDBContext, IEmailSender emailSender, IWebHostEnvironment webHostEnvironment, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _appDBContext = appDBContext;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
        }
        [Route("confirm")]
        [NonAction]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Unauthorized(user);

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return Ok(result.Succeeded);
        }
        [HttpPost]
        [Route("password")]
        public async Task<IActionResult> PasswordLink(EmailDTO _user)
        {
            string email = _user.Email;

            string confirmationUrl = $"https://localhost:7274/passwordreset/{email}";

            string resetLink = $"Please reset your password by <a href='{confirmationUrl}'>clicking here</a>.";

            await _emailSender.SendEmailAsync(_user.Email, "Booking Sucessful", "Please reset your password by clicking <a href='https://localhost:7274/passwordreset/'"+email+"> here. </a>");

            //await _emailSender.SendEmailAsync(email, "Password Reset", resetLink);

            return Ok();

        }
    }

}