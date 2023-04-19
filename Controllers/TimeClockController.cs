using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BJHRApp.Models;
using BJHRApp.Data;
using BJHRApp.Utilities;

namespace BJHRApp.Controllers;
[Route("users/timeclock")]
[SessionCheck]
public class TimeClockController : Controller
{
    private readonly ILogger<TimeClockController> _logger;
    private DBContext _context;
    public TimeClockController(ILogger<TimeClockController> logger, DBContext context)
    {
        _logger = logger;
        _context = context;
    }

    [ClaimCheck]
    [HttpGet("{UserId}")]
    public IActionResult TimeClockDashboard(int UserId)
    {
        ViewBag.Punches = new List<Punch>();
        ViewBag.LatestPunch = new Punch();
        List<Punch> punches = _context.Punches
                                .Where(p => p.UserId == UserId)
                                .OrderBy(p=>p.Time)
                                .ToList();

        if (punches.Any())
        {
            ViewBag.Punches = punches;
            ViewBag.LatestPunch = punches.Last();
        }
        return View();
    }

    [ClaimCheck]
    [HttpPost("{UserId}/punch")]
    public RedirectResult CreatePunch(int UserId)
    {
        if (!IsPunchValid(UserId))
        {
            return Redirect($"/users/timeclock/{UserId}");
        }
        Punch? latestPunch = _context.Punches
                                .Where(p=>p.UserId == UserId)
                                .OrderBy(p=>p.Time)
                                .LastOrDefault();
        if (latestPunch == null)
        {
            _context.Punches.Add(new Punch { UserId = UserId, ClockedIn = true });
        }
        else
        {
            _context.Punches.Add(new Punch { UserId = UserId, ClockedIn = !latestPunch.ClockedIn });
        }
        _context.SaveChanges();
        return Redirect($"/users/timeclock/{UserId}");
    }

    private bool IsPunchValid(int UserId)
    {
        //TODO: I will include a schedule check when they are implemented. - Josh
        Punch? punch = _context.Punches.Where(p => p.UserId == UserId).OrderBy(punch=>punch.Time).LastOrDefault();
        if (punch == null) 
        {
            Console.WriteLine("Punch is null");
            return true; 
        }
        Console.WriteLine("Punch was not null and got to ternary.");
        return (DateTime.UtcNow > punch.Time.AddMinutes(1) ? true : false);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}