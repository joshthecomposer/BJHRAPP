namespace BJHRApp.Models;
public class ShiftWithDateTime : BaseEntity
{
    public int UserId { get; set; }
    public string? Block { get; set; } 
    public DateTime In { get; set; }
    public DateTime Out { get; set; }

    public ShiftWithDateTime(Shift shift)
    {

        DateOnly Date = DateOnly.Parse(shift.Date!);
        Id = shift.Id;
        UserId = shift.UserId;
        Block = shift.Block;
        switch(shift.Block)
        {
            case "Open":
            In = new DateTime(Date.Year,Date.Month,Date.Day, 9,0,0).ToUniversalTime();
            Console.WriteLine($"Shift In from Constructor: {In}");
            Out = In.AddHours(8);
            Console.WriteLine($"Shift Out from Constructor: {Out}");
            break;

            case "Close":
            In = new DateTime(Date.Year,Date.Month,Date.Day, 14,0,0).ToUniversalTime();
            Console.WriteLine($"Shift In from Constructor: {In}");
            Out = In.AddHours(8);
            Console.WriteLine($"Shift Out from Constructor: {Out}");
            break;
        }
    }
}