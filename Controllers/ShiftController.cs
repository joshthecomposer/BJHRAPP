#pragma warning disable CS8602
using System.Diagnostics;
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
        
        List<User> Users = _context.Users.Where(u => u.Id >=0).ToList();
        // This should probably be a dictionary, but I'll change that as needed. Placeholder to get the juices flowin'.
        List<string> Block = new List<string>(){"In: 9:00 | Out: 17:00", "In: 14:00 | Out: 22:00"};
        ViewBag.Users = Users;
        ViewBag.PresetShifts = Block;
        ViewBag.Shifts = _context.Shifts.Where(s => s.In.Day == DateTime.UtcNow.Day).ToList();
        return View();
    }

    [HttpPost("/shift/create")]
    public IActionResult Create()
    {
        if(ModelState.IsValid)
        {
            string ShiftDate = Request.Form["ShiftDate"]!;
            int UserId = Int32.Parse(Request.Form["User"]!);
            Shift NewShift = new Shift();
            NewShift.UserId = UserId;
            DateOnly SelectedDate = DateOnly.Parse(ShiftDate);
            Console.WriteLine("Incoming UserId from Form: " + UserId);
            Console.WriteLine("NewShift.UserId: " + NewShift.UserId);

            if(Int32.Parse(Request.Form["Block"]!) == 0)
            {
                NewShift.In = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, 9,0,0).ToUniversalTime();
                NewShift.Out = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, 17,0,0).ToUniversalTime();
                _context.Add(NewShift);
                _context.SaveChanges();
                return Redirect($"/users/shift/{UserId}");
            }
            if(Int32.Parse(Request.Form["Block"]!) == 1)
            {
                NewShift.In = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, 14,0,0).ToUniversalTime();
                NewShift.Out = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, 22,0,0).ToUniversalTime();
                _context.Add(NewShift);
                _context.SaveChanges();
                return Redirect($"/users/shift/{UserId}");
            }
            Console.WriteLine("yay it's valid");

            return Redirect($"/users/shift/{UserId}");
        }
        else
        {
            return View("ShiftDashboard");
        }
    }
}