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
    [HttpGet("{userId}")]
    public IActionResult TimeClockDashboard(int userId)
    {
        ViewBag.Punches = new List<Punch>();
        List<Punch> punches = _context.Punches
                                .Where(p => p.UserId == userId)
                                .OrderBy(p => p.TimeIn)
                                .ToList();
        Punch latest = new Punch();
        if (punches.Any())
        {
            latest = punches.Last();
            ViewBag.Punches = punches;
        }
        ViewBag.Date = DateTime.Now;

        return View(latest);
    }

    [ClaimCheck]
    [HttpPost("{userId}/createfilter")]
    public RedirectResult CreateFilterUrl(object sender, EventArgs e, int userId)
    {
        string input = Request.Form["QueryDate"]!;
        DateTime date = DateTime.Parse(input);
        string url = $"/users/timeclock/{userId}/{date.Year}/{date.Month}/{date.Day}";
        return Redirect(url);
    }
    //TODO: Comgbing filter function into normal punch
    //TODO: Filter into a weekly view instead of daily view.
    [ClaimCheck]
    [HttpGet("{userId}/{year}/{month}/{day}")]
    public IActionResult FilterPunchView(int userId, int year, int month, int day)
    {
        DateTime queryDate = new DateTime(year, month, day);
        ViewBag.Punches = new List<Punch>();
        //TODO: See if we need to do a check for timeout as well.
        List<Punch> punches = _context.Punches.Where(p => p.UserId == userId && (p.TimeIn.Date == queryDate)).ToList();
        Punch latest = new Punch();
        //This checkIfAny is there so that the time puncher button relies only on the actual latest punch, not the latest in the view list
        var checkIfAny = _context.Punches.Where(p => p.UserId == userId).OrderBy(p => p.TimeIn).LastOrDefault()!;
        if (checkIfAny != null)
        {
            latest = checkIfAny;
        }
        if (punches.Any())
        {
            ViewBag.Punches = punches;
        }
        ViewBag.Date = new DateTime(year, month, day);
        return View("TimeClockDashboard",latest);
    }

    [ClaimCheck]
    [HttpPost("{userId}/punch")]
    public RedirectResult CreatePunch(int userId)
    {
        List<Punch> punches = _context.Punches
                                .Where(p => p.UserId == userId)
                                .OrderBy(p => p.TimeIn)
                                .ToList();
        if (!punches.Any())
        {
            _context.Punches.Add(new Punch {UserId = userId, TimeIn = DateTime.UtcNow });
            _context.SaveChanges();
            return Redirect($"/users/timeclock/{userId}");
        }
        Punch latest = punches.Last();
        if (DateTime.UtcNow < latest.UpdatedAt.AddMinutes(1))
        {
            return Redirect($"/users/timeclock/{userId}");
        }
        if (latest.TimeOut == null)
        {
            latest.TimeOut = DateTime.UtcNow;
            latest.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }
        else
        {
             _context.Punches.Add(new Punch {UserId = userId, TimeIn = DateTime.UtcNow });
             _context.SaveChanges();
        }
        return Redirect($"/users/timeclock/{userId}");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}