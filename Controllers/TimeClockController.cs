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
        List<Punch> punches = _context.Punches
                                .Where(p => p.UserId == UserId)
                                .OrderBy(p => p.TimeIn)
                                .ToList();
        Punch latest = new Punch();
        Console.WriteLine("TIME IN ON NULL PUNCH??? "+latest.TimeIn);
        if (punches.Any())
        {
            latest = punches.Last();
            ViewBag.Punches = punches;
        }

        return View(latest);
    }

    [ClaimCheck]
    [HttpPost("{UserId}/punch")]
    public RedirectResult CreatePunch(int UserId)
    {
        List<Punch> punches = _context.Punches
                                .Where(p => p.UserId == UserId)
                                .OrderBy(p => p.TimeIn)
                                .ToList();
        if (!punches.Any())
        {
            _context.Punches.Add(new Punch {UserId = UserId, TimeIn = DateTime.UtcNow });
            _context.SaveChanges();
            return Redirect($"/users/timeclock/{UserId}");
        }
        Punch latest = punches.Last();
        // if (DateTime.UtcNow > punchTime.AddMinutes(1))

        if (latest.TimeOut == null)
        {
            latest.TimeOut = DateTime.UtcNow;
            _context.SaveChanges();
        }
        else
        {
             _context.Punches.Add(new Punch {UserId = UserId, TimeIn = DateTime.UtcNow });
             _context.SaveChanges();
        }
        return Redirect($"/users/timeclock/{UserId}");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}