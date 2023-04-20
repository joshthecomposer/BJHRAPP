#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BJHRApp.Models;
public class CustomPunch : BaseEntity
{
    [Required(ErrorMessage = "Field required.")]
    public string Date { get; set; }
    [Required(ErrorMessage = "Field required.")]
    public string TimeIn { get; set; }
    [Required(ErrorMessage = "Field required.")]
    public string TimeOut { get; set; }
    [Required(ErrorMessage = "Field required.")]
    public int UserId { get; set; }
}