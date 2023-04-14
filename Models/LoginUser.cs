#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace BJHRApp.Models;
public class LoginUser
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MinLength(8)]
    public string Password { get; set; }
}