#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
using BJHRApp.Models;
namespace BJHRApp.Data;
public class DBContext : DbContext 
{   
    public DBContext(DbContextOptions options) : base(options) { }      
    public DbSet<User> Users { get; set; }
    public DbSet<Punch> Punches { get; set; }
    public DbSet<Shift> Shifts { get;set; }
    public DbSet<HomeAddress> HomeAddresses { get; set; }
    public DbSet<Location> Locations { get; set; }
}