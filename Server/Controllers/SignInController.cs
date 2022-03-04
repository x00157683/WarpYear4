﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Controllers { 

   
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
    private SignInManager<IdentityUser> _signInManager;
    private UserManager<IdentityUser> _userManager;
    private IConfiguration _configuration;

    public SignInController(SignInManager<IdentityUser> signInManager,UserManager<IdentityUser> userManager,IConfiguration configuration)
        {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;


        }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> SignIn([FromBody] User user)
    {
        string username = user.EmailAddress;
        string password = user.Password;

        Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(username, password, false, false);

        if (signInResult.Succeeded)
        {
            IdentityUser identityUser = await _userManager.FindByNameAsync(username);
            string JSONWebTokenAsString = await GenerateJSONWebToken(identityUser);
            return Ok(JSONWebTokenAsString);
        }
        else
        {
            return Unauthorized(user);
        }
    }


    [NonAction]
    [ApiExplorerSettings(IgnoreApi =true)]
    private async Task<string> GenerateJSONWebToken(IdentityUser identityUser)
    {

        SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new()
        {
            new Claim(JwtRegisteredClaimNames.Sub,identityUser.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier,identityUser.Id)
        };

        IList<string> roleNames = await _userManager.GetRolesAsync(identityUser);
        claims.AddRange(roleNames.Select(roleName => new Claim(ClaimsIdentity.DefaultRoleClaimType, roleName)));

        JwtSecurityToken jwtSecurityToken = new
        (
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            claims,
            null,
            expires: DateTime.UtcNow.AddDays(74),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

    }






    }
}
