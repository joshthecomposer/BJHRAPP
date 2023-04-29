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
    [HttpPost("create")]
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
        List<User> Users = _context.Users.Where(u => u.Id >=0).ToList();
        List<string> Blocks = new List<string>(){"Open", "Close"};
        ViewBag.Users = Users;
        ViewBag.Blocks = Blocks;

        Shift? OneShift = _context.Shifts.FirstOrDefault(s => s.Id == ShiftId);
        return View(OneShift);
    }

    [HttpPost("update/{ShiftId}")]
    public IActionResult UpdateShift(Shift newShift, int ShiftId)
    {
        Shift? OldShift = _context.Shifts.FirstOrDefault(s => s.Id == ShiftId);
        int UserId = (Int32)HttpContext.Session.GetInt32("UserId")!;
        bool IsScheduled = _context.Shifts.Where(s => s.UserId == newShift.UserId && s.Date == newShift.Date && s.Block == newShift.Block).Any();

        if(ModelState.IsValid && !IsScheduled)
        {
            OldShift.UserId = newShift.UserId;
            OldShift.Date = newShift.Date;
            OldShift.Block = newShift.Block;
            OldShift.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return RedirectToAction("ShiftAll", new { UserId = newShift.UserId });
        }

        return RedirectToAction("ShiftEdit", new { ShiftId = OldShift.Id, errors = ModelState });
    }

    [HttpPost("delete/{ShiftId}")]
    public IActionResult DeleteShift(int ShiftId)
    {
        Shift? DeletedShift = _context.Shifts.SingleOrDefault(s => s.Id == ShiftId);
        int UserId = DeletedShift.UserId;
        if(DeletedShift!=null)
        {
            _context.Shifts.Remove(DeletedShift);
            _context.SaveChanges();
            return Redirect($"/users/shift/all/{UserId}");
        }
        return View("ShiftAll");
    }
}