#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BJHRApp.Models;
public class Punch : BaseEntity
{

    public DateTime TimeIn { get; set; }
    public DateTime? TimeOut { get; set; }
    [Required]
    public int UserId { get; set; }

    //DB Associations
    public User? User { get; set; }
}