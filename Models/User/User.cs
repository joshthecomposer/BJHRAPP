#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BJHRApp.Utilities;

namespace BJHRApp.Models;
public class User : BaseEntity
{
    [Required(ErrorMessage = "Field required")]
    [MinLength(2, ErrorMessage = "At least 2 characters")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Field required")]
    [MinLength(2, ErrorMessage = "At least 2 characters")]
    public string LastName { get; set; }
    [UniqueEmail]
    [EmailAddress(ErrorMessage = "Invalid email")]
    public string Email { get; set; }
    public int AdminLevel { get; set; } = 0;
    [Required(ErrorMessage = "Field required")]
    [MinLength(8, ErrorMessage = "At least 8 characters")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Field required")]
    [NotMapped]
    [Compare("Password")]
    public string Confirm { get; set; }

    //Foreign Keys
    public int? LocationId { get; set; }

    // DB Associations 
    public List<Punch> Punches = new List<Punch>();
    public List<Shift> Shifts = new List<Shift>();
    public HomeAddress? HomeAddress { get; set; }
    public Location? Location { get; set; }
}