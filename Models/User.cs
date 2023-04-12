#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJHRApp.Models;
public class User : BaseEntity
{
    [Required]
    [MinLength(4)]
    public string FirstName { get; set; }
    [Required]
    [MinLength(4)]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    [UniqueEmail]
    public string Email { get; set; }
    [Required] 
    public int AdminLevel { get; set;}
    [Required]
    [MaxLength(8)]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    public string Confirm { get; set; }

    //One user to many punches
    public List<Punch> Punches = new List<Punch>();
}