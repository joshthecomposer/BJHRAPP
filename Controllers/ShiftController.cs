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
        List<User> Users = _context.Users.Where(u => u.Id >=0).ToList();
        List<string> Blocks = new List<string>(){"Open", "Close"};
        ViewBag.Users = Users;
        ViewBag.Blocks = Blocks;
        return View();
    }

    [SessionCheck]
    [ClaimCheck]
    [HttpGet("all/{UserId}")]
    public IActionResult ShiftAll(int UserId)
    {
        ViewBag.AllShifts = _context.Shifts.Where(s => s.UserId == UserId).ToList();
        return View();
    }

    [SessionCheck]
    [HttpPost("/shift/create")]
    public IActionResult CreateShift(Shift shift)
    {
        int UserId = shift.UserId;
        bool IsScheduled = _context.Shifts.Where(s => s.UserId == shift.UserId && s.Date == shift.Date).Any();
        Console.WriteLine($"IsScheduled: {IsScheduled}");
        if(ModelState.IsValid && IsScheduled == false)
        {
            Console.WriteLine("Model State was valid!");
            _context.Add(shift);
            _context.SaveChanges();
            return Redirect($"/users/shift/{UserId}");
        }
        else
        {
            // TODO: Eventually validate this with AJAX
            Console.WriteLine($"User already scheduled on {shift.Date}");
            Console.WriteLine("OR Something went wrong rawrxd");
            return Redirect($"/users/shift/{UserId}");
        }
    }

    [HttpGet("edit/{ShiftId}")]
    public IActionResult ShiftEdit(int ShiftId)
    {
        Console.WriteLine($"############ SHIFT DATE : ");
        return View();
    }

    [HttpPost("/shift/delete/{ShiftId}")]
    public IActionResult DeleteShift(int ShiftId)
    {
        Shift? DeletedShift = _context.Shifts.SingleOrDefault(s => s.Id == ShiftId);
        int UserId = DeletedShift.UserId;
        if(DeletedShift!=null)
        {
            _context.Shifts.Remove(DeletedShift);
            _context.SaveChanges();
            return RedirectToAction($"/users/shift/all/{UserId}");
        }
        return View("ShiftAll");
    }
    // TODO Continue refactoring this method. May need to abstract it into another class so that this method can be called more easily from other controllers.
    private Dictionary<int, DateTime[]> BuildShift(string date, string block)
    {   
        Dictionary<int, DateTime[]> ShiftInOut = new Dictionary<int, DateTime[]>(); 
        DateOnly ShiftScheduledDate = DateOnly.Parse(date);
        switch(block)
        {
            case "Open":
            Console.WriteLine("Case Open");
            // We are going to need to put this switch case in a for loop
            // We will also need to pass in the list of shift dates and blocks
            // We'll parse everything into the appropriate In/Out DateTimes and go from there.
            break;

            case "Close":
            Console.WriteLine("Case Close");
            // NewShift.In = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, 14, 0, 0).ToUniversalTime();
            // NewShift.Out = NewShift.In.AddHours(8);
            break;
        }
        return ShiftInOut;
    }
}