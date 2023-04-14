#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace BJHRApp.Models;
public class LoginUser
{
    [Required(ErrorMessage="Field required")]
    [EmailAddress(ErrorMessage ="Invalid email")]
    public string Email { get; set; }
    [Required(ErrorMessage="Field required")]
    public string Password { get; set; }
}