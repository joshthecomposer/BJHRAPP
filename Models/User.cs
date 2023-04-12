#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJHRApp.Models;
public class User
{
    [Key]
    public int UserId { get; set; }
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
    //TODO: Is this truly required on creation or can be defaulted to 0 and assigned later?
    //TODO: message-Brian I agree, this probably isn't actually required. Could likely set this to a default user value upon creation.
    public int AdminLevel { get; set;}
    [Required]
    [MaxLength(8)]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    public string Confirm { get; set; }

    public DateTime CreatedAt = DateTime.Now;
    public DateTime UpdatedAt = DateTime.Now;

    //One user to many punches
    public List<Punch> Punches = new List<Punch>();
}