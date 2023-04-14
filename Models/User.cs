#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJHRApp.Models;
public class User : BaseEntity
{
    [Required(ErrorMessage="Field required")]
    [MinLength(2, ErrorMessage ="At least 2 characters")]
    public string FirstName { get; set; }
    [Required(ErrorMessage="Field required")]
    [MinLength(2, ErrorMessage ="At least 2 characters")]
    public string LastName { get; set; }
    [UniqueEmail]
    [EmailAddress(ErrorMessage ="Invalid email")]
    public string Email { get; set; }
    [Required]
    //TODO: Is this truly required on creation or can be defaulted to 0 and assigned later?
    //TODO: message-Brian I agree, this probably isn't actually required. Could likely set this to a default user value upon creation.
    public int AdminLevel { get; set;}
    [Required(ErrorMessage="Field required")]
    [MinLength(8, ErrorMessage ="At least 8 characters")]
    public string Password { get; set; }
    [Required (ErrorMessage="Field required")]
    [NotMapped]
    [Compare("Password")]
    public string Confirm { get; set; }

    //One user to many punches
    public List<Punch> Punches = new List<Punch>();
}