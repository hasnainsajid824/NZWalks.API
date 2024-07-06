using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            var user = new IdentityUser{
                UserName = register.Username,
                Email = register.Email,
            };              
            var identityresult = await userManager.CreateAsync(user, register.Password);

            if (identityresult.Succeeded && register.Roles.Any())
            {
                if (register.Roles != null)
                {
                    await userManager.AddToRolesAsync(user, register.Roles);
                    if(identityresult.Succeeded)
                    {
                        return Ok("User Was Successfully Registered !");
                    }
                }
            }

            return BadRequest("Something went wrong !");
        }

        [HttpPost]
        [Route("Login")]
        public async Task <IActionResult> Login([FromBody] LoginDto login)
        {
            var user = await userManager.FindByEmailAsync(login.Email); 

            if (user != null)
            {
                var check = await userManager.CheckPasswordAsync(user ,login.Password);
                if (check)
                {
                    var roles  = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var token = tokenRepository.CreateJWTToken(user, roles.ToList());
                        var loginResponse = new LoginResponseDto{
                            JWTToken = token
                        };
                        return Ok(loginResponse);
                    }
                }
            }
            return BadRequest("Email or Password is Incorrect");
        }
    }
}