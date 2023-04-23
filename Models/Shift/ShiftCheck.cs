using System.ComponentModel.DataAnnotations;

namespace BJHRApp.Models;

public class ShiftCheck
{
    [Required]
    public string? ShiftDate { get;set; }

    [Required]
    public int Block { get;set; }

    [Required]
    public int UserId { get;set; }
}