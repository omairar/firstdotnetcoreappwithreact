using BOL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        SignInManager<SSUser> signInManager;
        UserManager<SSUser> userManager;

        public AccountsController(SignInManager<SSUser> _signInManager, UserManager<SSUser> _userManager)
        {
            signInManager = _signInManager;
            userManager = _userManager;
        }

        [HttpPost("logout")]

        public async Task<IActionResult> LogOut()
        {


            await signInManager.SignOutAsync();

            return NoContent();



        }

        [HttpPost("login")]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByNameAsync(model.UserName);
                    var userResult = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (userResult.Succeeded)
                    {
                        var roles = await userManager.GetRolesAsync(user);

                        //step 1: Creating Claims
                        IdentityOptions identityOptions = new IdentityOptions();
                        var claims = new Claim[]
                        {
                            new Claim(identityOptions.ClaimsIdentity.UserIdClaimType, user.Id),
                            new Claim(identityOptions.ClaimsIdentity.UserNameClaimType, user.UserName),
                            new Claim(identityOptions.ClaimsIdentity.RoleClaimType, roles[0]),
                        };

                        //Step - 2: Create signingKey from Secretkey
                        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is the JWT Security Token Authentication"));

                        //Step -3: Create signingCredentials from signingKey with HMAC algorithm
                        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

                        //Step - 4: Create JWT with signingCredentials, IdentityClaims & expire duration.
                        var jwt = new JwtSecurityToken(signingCredentials: signingCredentials,
                                                        expires: DateTime.Now.AddMinutes(30), claims: claims);

                        //Step - 5: Finally write the token as response with OK().
                        return Ok(new { tokenJwt = new JwtSecurityTokenHandler().WriteToken(jwt), id = user.Id, userName = user.UserName, role = roles[0] });


                       
                    }
                    else
                    {
                        return BadRequest("Invalid Username or Password");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                var msg = (e.InnerException != null) ? (e.InnerException.Message) : (e.Message);

                return StatusCode(500, "Internal Server Error" + msg);
            }
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new SSUser()
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        DOB = model.DOB,

                    };

                    var userResult = await userManager.CreateAsync(user, model.Password);
                    if (userResult.Succeeded)
                    {
                        var roleResult = await userManager.AddToRoleAsync(user, "User");

                        if (roleResult.Succeeded)
                        {
                            return Ok(user);

                        }
                        else
                        {
                            return BadRequest(roleResult.Errors);
                        }
                    }
                    else
                    {
                        return BadRequest(userResult.Errors);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                var msg = (e.InnerException != null) ? (e.InnerException.Message) : (e.Message);

                return StatusCode(500, "Admin is working on it " + msg);
            }
        }
    }
}
