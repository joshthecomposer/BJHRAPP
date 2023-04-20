#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJHRApp.Models;
public class HomeAddress : Address
{
    [Required]
    public int UserId { get; set; }

    //DB Association
    public User User { get; set; }
}