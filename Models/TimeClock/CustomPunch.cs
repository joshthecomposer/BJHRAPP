#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BJHRApp.Models;
public class CustomPunch
{
    [Required]
    public string Date { get; set; }
    [Required]
    public string TimeIn { get; set; }
    [Required]
    public string TimeOut { get; set; }
    [Required]
    public int UserId { get; set; }
}