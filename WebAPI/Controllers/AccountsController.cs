using BOL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

                    var userResult = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                    if (userResult.Succeeded)
                    {
                        var user = await userManager.FindByNameAsync(model.UserName);
                        var role = await userManager.GetRolesAsync(user);
                        return Ok(new { id = user.Id, userName = user.UserName, role = role[0] });
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
