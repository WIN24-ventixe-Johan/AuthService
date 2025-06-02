using JWT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VerifyController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public VerifyController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    [HttpPost]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email!);
        if (user == null)
            return NotFound("Ingen användare hittades");

        var result = await _userManager.ConfirmEmailAsync(user, model.Token!);
        if (!result.Succeeded)
            return BadRequest("Verifikationen misslyckades");

            return Ok("Verifieringen lyckades");
     
    }

}
