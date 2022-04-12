using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]

public class UserController : ControllerBase
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;

    public UserController(SignInManager<AppUser> signInManager,UserManager<AppUser> userManager,IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
    }
    [AllowAnonymous]
    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] UserDTO userForRegistration)
    {


        string password = userForRegistration.Password;

        if (userForRegistration == null || !ModelState.IsValid)
            return BadRequest();
        var user = new AppUser { UserName = userForRegistration.EmailAddress, Email = userForRegistration.EmailAddress,
                                FirstName = userForRegistration.FirstName,LastName=userForRegistration.LastName,
                                PhoneNumber=userForRegistration.PhoneNumber};

        

        IdentityResult identityResult = await _userManager.CreateAsync(user, password);
        if (identityResult.Succeeded == true)
        {
            return Ok(identityResult.Succeeded);
        }
        else
        {
            string errorsToReturn = "Register failed with the following errors";

            foreach (var err in identityResult.Errors)
            {
                errorsToReturn += Environment.NewLine;
                errorsToReturn += $"Error Code: {err.Code} - {err.Description}";
            }

            return StatusCode(StatusCodes.Status500InternalServerError,errorsToReturn);
        }
        return StatusCode(201);
    }

    [AllowAnonymous]
    [Route("signin")]
    [HttpPost]
    public async Task<IActionResult> SignIn([FromBody] User userForSignIn)
    {
        string username = userForSignIn.EmailAddress;
        string password = userForSignIn.Password;

        Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(username,password,false,false);


        if (signInResult.Succeeded == true)
        {
            AppUser identityUser = await _userManager.FindByIdAsync(username);
            string JSONWebTokenAsString = await GenerateJSONWebToken(identityUser);
            return Ok(JSONWebTokenAsString);
        }
        else
        {
            return Unauthorized(userForSignIn);
        }
      

        return StatusCode(201);
    }

    [NonAction]
    [ApiExplorerSettings(IgnoreApi =true)]
    private async Task<string> GenerateJSONWebToken(AppUser identityUser)
    {

        SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[ "Jwt:Key"]));
        SigningCredentials credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        //Who is the person claiming to be?
        List<Claim> claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Sub,identityUser.Email),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, identityUser.Id)
        };

        IList<string> roleNames = await _userManager.GetRolesAsync(identityUser);
        claims.AddRange(roleNames.Select(roleName => new Claim(ClaimsIdentity.DefaultRoleClaimType, roleName)));

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            claims,
            null,
            expires: DateTime.UtcNow.AddDays(72),
            signingCredentials: credentials

            ) ;
        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

    }

    [NonAction]
    [ApiExplorerSettings(IgnoreApi = true)]
    private async Task<IdentityUser> GetUserByUserId(string id)
    {
        IdentityUser identityUser = await _userManager.FindByEmailAsync(id);

        return identityUser;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        IdentityUser identityUser = await GetUserByUserId(id);
        return Ok(identityUser);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
            //List<AppUser> usersFromDatabase = (List<AppUser>)await _userManager.Users.FirstOrDefault();

        var users = await _userManager.Users.ToListAsync();

        return Ok(users);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        try
        {

            AppUser user = await _userManager.FindByEmailAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {


                    return NoContent();
                }
                else
                    return StatusCode(500, $"Something went wrong on our side. Please contact the administrator. Error message: {result.ToString}.");
            }
            else
                return StatusCode(500, "User Not Found");
      
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Something went wrong on our side. Please contact the administrator. Error message: {e.Message}.");
        }
    }

    [Route("update")]
    [HttpPost]
    public async Task<IActionResult> UpdateUser([FromBody] UserDTO userForRegistration)
    {
        AppUser user = await _userManager.FindByIdAsync(userForRegistration.EmailAddress);


        if (user != null)
        {
            user.UserName = userForRegistration.EmailAddress;
            user.Email = userForRegistration.EmailAddress;
            user.FirstName = userForRegistration.FirstName;
            user.LastName = userForRegistration.LastName;
            user.PhoneNumber = userForRegistration.PhoneNumber;
            IdentityResult identityResult = await _userManager.UpdateAsync(user);

            if (identityResult.Succeeded == true)
            {
                return Ok(identityResult.Succeeded);
            }
            else
            {
                string errorsToReturn = "Update failed with the following errors";

                foreach (var err in identityResult.Errors)
                {
                    errorsToReturn += Environment.NewLine;
                    errorsToReturn += $"Error Code: {err.Code} - {err.Description}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorsToReturn);
            }
            
        }

        else
        {
            return StatusCode(201);

        }
    
    }



}