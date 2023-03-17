#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJHRApp.Models;
public class Punch
{
    [Key]
    public int PunchId { get; set; }
    [Required]
    public DateTime Time { get; set; } = DateTime.Now;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    //DB Relationship
    public User User { get; set; }
}