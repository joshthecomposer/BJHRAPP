#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace BJHRApp.Models;

public class UpdateUser : BaseEntity
{
    [Required(ErrorMessage="Field required")]
    [MinLength(2, ErrorMessage ="At least 2 characters")]
    public string FirstName { get; set; }
    [Required(ErrorMessage="Field required")]
    [MinLength(2, ErrorMessage ="At least 2 characters")]
    public string LastName { get; set; }
}