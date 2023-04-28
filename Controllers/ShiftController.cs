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
        List<Shift> AllShifts = _context.Shifts.Where(s => s.UserId == UserId).ToList();
        List<ShiftWithDateTime> AllShiftsWithDateTime = new List<ShiftWithDateTime>();
        for(int i=0; i<AllShifts.Count; i++)
        {
            ShiftWithDateTime NewShiftWithDateTime = new ShiftWithDateTime(AllShifts[i]);
            AllShiftsWithDateTime.Add(NewShiftWithDateTime);
        }
        ViewBag.AllShiftsWithDateTime = AllShiftsWithDateTime;
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
=======
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
    // TODO: Refactor this method to translate these shifts into date times 
    // and add them to the ViewBag.
    // private Shift BuildShift(string date, int shiftOption, int userId)
    // {
    //     Shift NewShift = new Shift();
    //     NewShift.UserId = userId;
    //     DateOnly SelectedDate = DateOnly.Parse(date);
    //     switch(shiftOption)
    //     {
    //         case 0:
    //         Console.WriteLine("First Shift selected");
    //         NewShift.In = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, 9, 0 , 0).ToUniversalTime();
    //         NewShift.Out = NewShift.In.AddHours(8);
    //         break;

    //         case 1:
    //         Console.WriteLine("Second Shift selected");
    //         NewShift.In = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, 14, 0, 0).ToUniversalTime();
    //         NewShift.Out = NewShift.In.AddHours(8);
    //         break;
    //     }
    //     return NewShift;
    // }
}