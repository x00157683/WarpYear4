using System.Threading.Tasks;
using Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    public class EmailController : Controller
    {
        private UserManager<AppUser> userManager;
        public EmailController(UserManager<AppUser> usrMgr)
        {
            userManager = usrMgr;
        }

        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return Unauthorized(user);

            var result = await userManager.ConfirmEmailAsync(user, token);
            return Ok(result.Succeeded);
        }
    }
}