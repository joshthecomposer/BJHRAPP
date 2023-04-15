#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJHRApp.Models;
public class Punch : BaseEntity
{
    [Required]
    public DateTime Time { get; set; } = DateTime.Now;
    [Required]
    public int UserId { get; set; }

    //DB Associations
    public User User { get; set; }
}