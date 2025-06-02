using System.ComponentModel.DataAnnotations;

namespace JWT.Models;

public class RegisterModel
{
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    [Compare("Password", ErrorMessage = "Passwords dont match!")]
    public string ConfirmPassword { get; set; } = null!;

}
