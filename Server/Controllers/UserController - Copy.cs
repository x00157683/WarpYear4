//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Shared.Models;

//[Route("api/accounts")]
//[ApiController]
//public class AccountsController : ControllerBase
//{
//    private readonly UserManager<IdentityUser> _userManager;
//    public AccountsController(UserManager<IdentityUser> userManager)
//    {
//        _userManager = userManager;
//    }
//    [HttpPost("Registration")]
//    public async Task<IActionResult> RegisterUser([FromBody] UserDTO userForRegistration)
//    {
//        if (userForRegistration == null || !ModelState.IsValid)
//            return BadRequest();
//        var user = new IdentityUser { UserName = userForRegistration.EmailAddress, Email = userForRegistration.EmailAddress };

//        var result = await _userManager.CreateAsync(user, userForRegistration.Password);
//        if (!result.Succeeded)
//        {
//            var errors = result.Errors.Select(e => e.Description);
//            return BadRequest(new RegistrationResponseDto { Errors = errors });
//        }

//        return StatusCode(201);
//    }
//}