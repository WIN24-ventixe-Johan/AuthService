using JWT.Models;
using JWT.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace JWT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Skyddar alla endpoints per default
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EmailService _emailService;

        public RegisterController(UserManager<ApplicationUser> userManager, EmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

       
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmationLink = $"https://victorious-sky-0d7ac1303.6.azurestatiapps.net/verify?email={user.Email}&token={HttpUtility.UrlEncode(token)}";

           
            await _emailService.SendEmailAsync(
                user.Email,
                "Bekräfta din e-post",
                $"<h2>Klicka här för att bekräfta din e-post:</h2><a href='{confirmationLink}'>Bekräfta</a>"
            );

            return Ok($"Registreringen lyckades för {user.Email}! Kolla din e-post för att verifiera");
        }

        
        [HttpGet("userinfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                Name = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                Roles = roles
            });
        }
    }
}
