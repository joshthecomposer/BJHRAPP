#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace BJHRApp.Models;

public class ShiftCheck
{
    [Required]
    public string? ShiftDate { get;set; }

    [Required]
    public Dictionary<int,string> Block { get;set; }

    [Required]
    public int UserId { get;set; }

    // TODO: Add the shift constructor here after I figure out how exactly I want to refactor this -Brian
}