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
        Dictionary<int,string> Blocks = new Dictionary<int,string>();
        Blocks.Add(0,"Open");
        Blocks.Add(1,"Close");
        ViewBag.ShiftUsers = ShiftUsers;
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
    public IActionResult CreateShift(ShiftCheck shiftCheck)
    {
        int UserId = shiftCheck.UserId;

        DateOnly UniqueDate = DateOnly.Parse(shiftCheck.ShiftDate!);
        bool IsScheduled = _context.Shifts.Where(s => s.UserId == shiftCheck.UserId && s.In.Day == UniqueDate.Day).Any();
        Console.WriteLine($"IsScheduled: {IsScheduled}");
        // TODO: For some reason the model will not validate. Tried fixing, but need to research more.
        if(ModelState.IsValid && !IsScheduled)
        {
            Console.WriteLine("Model State was valid!");
            Shift NewShift = BuildShift(shiftCheck.ShiftDate!, shiftCheck.Block.ElementAt(0).Key, shiftCheck.UserId);
            _context.Add(NewShift);
            _context.SaveChanges();
            return RedirectToAction($"/users/shift/{UserId}");
        }
        else
        {
            // TODO: Eventually validate this with AJAX
            Console.WriteLine($"User already scheduled on {UniqueDate}");
            Console.WriteLine("OR Something went wrong rawrxd");
            return Redirect($"/users/shift/{UserId}");
        }
    }

    [HttpGet("edit/{ShiftId}")]
    public IActionResult ShiftEdit(int ShiftId)
    {
        Dictionary<int,string> ShiftStart = new Dictionary<int,string>();
        ShiftStart.Add(0,"Open");
        ShiftStart.Add(1,"Close");
        Shift? ShiftToEdit = _context.Shifts.FirstOrDefault(s => s.Id == ShiftId);
        ShiftCheck ShiftCheckToEdit = new ShiftCheck(); 
        ShiftCheckToEdit.ShiftDate = ShiftToEdit.In.ToShortDateString();
        // Consider 
        Console.WriteLine($"############ SHIFT DATE : {ShiftCheckToEdit.ShiftDate}");
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