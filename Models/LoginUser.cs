#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJHRApp.Models;
class LoginUser
{
    [Required(ErrorMessage="Field required")]
    [EmailAddress(ErrorMessage ="Invalid email")]
    public string Email { get; set; }
    [Required(ErrorMessage="Field required")]
    public string Password { get; set; }
}