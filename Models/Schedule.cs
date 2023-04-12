#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJHRApp.Models;
public class Schedule : BaseEntity
{
    [Required]
    public DateTime ScheduledDay { get; set; }
    [Required]
    public DateTime TimeIn { get; set; }
    [Required]
    public DateTime TimeOut { get; set; }

}