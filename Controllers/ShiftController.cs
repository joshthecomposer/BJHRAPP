#pragma warning disable CS8602
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BJHRApp.Models;
using BJHRApp.Utilities;
using BJHRApp.Data;

namespace BJHRApp.Controllers;

[SessionCheck]
[Route("users/shift")]
public class ShiftController : Controller
{
    // Instantiate the Logger
    private readonly ILogger<ShiftController> _logger;
    // Instantiate the DBContext
    private DBContext _context;
    // Inject the Logger and DBContext via the constructor
    public ShiftController(ILogger<ShiftController> logger, DBContext context)
    {
        _logger = logger;
        _context = context;
    }
    [SessionCheck]
    [HttpGet("{UserId}")]
    public IActionResult ShiftDashboard(int UserId)
    {
        List<User> ShiftUsers = _context.Users.Where(u => u.Id >=0).ToList();
        // This should probably be a dictionary, but I'll change that as needed. Placeholder to get the juices flowin'.
        List<string> Block = new List<string>(){"Opening", "Closing"};
        ViewBag.ShiftUsers = ShiftUsers;
        ViewBag.PresetShifts = Block;
        return View();
    }

    [SessionCheck]
    [HttpPost("/shift/create")]
    public IActionResult CreateShift(ShiftCheck shiftCheck)
    {
        int UserId = shiftCheck.UserId;

        DateOnly UniqueDate = DateOnly.Parse(shiftCheck.ShiftDate!);
        bool IsScheduled = _context.Shifts.Where(s => s.UserId == shiftCheck.UserId && s.In.Day == UniqueDate.Day).Any();
        Console.WriteLine($"IsScheduled: {IsScheduled}");

        if(ModelState.IsValid && !IsScheduled)
        {
            Shift NewShift = BuildShift(shiftCheck.ShiftDate!, shiftCheck.Block, shiftCheck.UserId);
            _context.Add(NewShift);
            _context.SaveChanges();
            return Redirect($"/users/shift/{UserId}");
        }
        // TODO: Eventually validate this with AJAX
        Console.WriteLine($"User already scheduled on {UniqueDate}");
        return Redirect($"/users/shift/{UserId}");
    }

    private Shift BuildShift(string date, int shiftOption, int userId)
    {
        Shift NewShift = new Shift();
        NewShift.UserId = userId;
        DateOnly SelectedDate = DateOnly.Parse(date);
        switch(shiftOption)
        {
            case 0:
            Console.WriteLine("First Shift selected");
            NewShift.In = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, 9, 0 , 0).ToUniversalTime();
            NewShift.Out = NewShift.In.AddHours(8);
            break;

            case 1:
            Console.WriteLine("Second Shift selected");
            NewShift.In = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, 14, 0, 0).ToUniversalTime();
            NewShift.Out = NewShift.In.AddHours(8);
            break;
        }
        return NewShift;
    }
}