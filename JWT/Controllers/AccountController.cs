//using JWT.Models;
//using JWT.Services;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using System.Web;

//[ApiController]
//[Route("api/[controller]")]
//public class AccountController : ControllerBase
//{
//    private readonly UserManager<ApplicationUser> _userManager;
//    private readonly EmailService _emailService;

//    public AccountController(UserManager<ApplicationUser> userManager, EmailService emailService)
//    {
//        _userManager = userManager;
//        _emailService = emailService;
//    }

//    [AllowAnonymous]
//    [HttpPost("register")]
//    public async Task<IActionResult> Register(RegisterModel model)
//    {
//        var user = new ApplicationUser
//        {
//            UserName = model.Email,
//            Email = model.Email,
//            FirstName = model.FirstName,
//            LastName = model.LastName,
//        };

//        var result = await _userManager.CreateAsync(user, model.Password);
//        if (!result.Succeeded)
//            return BadRequest(result.Errors);

//        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
//        var confirmationLink = $"https://yourfrontendurl.com/verify?email={user.Email}&token={HttpUtility.UrlEncode(token)}";

//        await _emailService.SendEmailAsync(
//            user.Email,
//            "Bekräfta din e-post",
//            $"<h2>Klicka här för att bekräfta din e-post:</h2><a href='{confirmationLink}'>Bekräfta</a>"
//        );

//        return Ok($"Registreringen lyckades för {user.Email}! Kolla din e-post för att verifiera");
//    }

//    [Authorize]
//    [HttpGet("userinfo")]
//    public async Task<IActionResult> GetUserInfo()
//    {
//        var user = await _userManager.GetUserAsync(User);
//        if (user == null) return Unauthorized();

//        var roles = await _userManager.GetRolesAsync(user);

//        return Ok(new
//        {
//            Name = $"{user.FirstName} {user.LastName}",
//            Email = user.Email,
//            Roles = roles
//        });
//    }
//}
