#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJHRApp.Models;
public class Location : Address
{
    //DB Association
    public List<User> Users { get; set; } = new List<User>();
}