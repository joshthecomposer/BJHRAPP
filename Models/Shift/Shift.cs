namespace BJHRApp.Models;

public class Shift : BaseEntity
{
    public string? Date { get; set; }
    public string? Block { get; set; }

    public int UserId { get; set; }

    // DB Associations
    public User? User { get; set; }
}