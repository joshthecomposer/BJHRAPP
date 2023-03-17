#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace BJHRApp.Models;
public class DBContext : DbContext 
{   
    public DBContext(DbContextOptions options) : base(options) { }      
    public DbSet<User> Users { get; set; }
    public DbSet<Punch> Punches { get; set; }
}