using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BJHRApp.Models;
using BJHRApp.Data;
using BJHRApp.Utilities;
using System.Text.Json;

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
    public RedirectResult TimeClockDashboard(int userId)
    {
        //TODO: GET RID OF THIS
        return Redirect($"{userId}/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}");
    }

    [ClaimCheck]
    [HttpPost("{userId}/createfilter")]
    public RedirectResult CreateFilterUrl(object sender, EventArgs e, int userId)
    {
        string input = Request.Form["QueryDate"]!;
        if (input == String.Empty)
        {
            return Redirect($"/users/timeclock/{userId}");
        }
        DateTime date = DateTime.Parse(input);
        string url = $"/users/timeclock/{userId}/{date.Year}/{date.Month}/{date.Day}";
        return Redirect(url);
    }
    //TODO: Filter into a weekly view instead of daily view.
    [ClaimCheck]
    [HttpGet("{userId}/{year}/{month}/{day}")]
    public IActionResult FilterPunchView(int userId, int year, int month, int day)
    {
        DateTime queryDate = new DateTime(year, month, day);
        ViewBag.Punches = new List<Punch>();
        //TODO: If someone clocks in at 11pm and out after 12am the next day, the timein will show that punch for the previous day. 
        //Determine whether this is bad or not
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
        return View("TimeClockDashboard", latest);
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
            _context.Punches.Add(new Punch { UserId = userId, TimeIn = DateTime.UtcNow });
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
            _context.Punches.Add(new Punch { UserId = userId, TimeIn = DateTime.UtcNow });
            _context.SaveChanges();
        }
        return Redirect($"/users/timeclock/{userId}");
    }

    [HttpPost("punch/custom")]
    public IActionResult CreateCustomPunch(CustomPunch input)
    {
        if (ModelState.IsValid && HttpContext.Session.GetInt32("UserId") == input.UserId)
        {
            DateOnly date = DateOnly.Parse(input.Date);
            TimeOnly timeIn = TimeOnly.Parse(input.TimeIn);
            TimeOnly timeOut = TimeOnly.Parse(input.TimeOut);
            Punch ingoing = new Punch
            {
                TimeIn = new DateTime(date.Year, date.Month, date.Day, timeIn.Hour, timeIn.Minute, 0).ToUniversalTime(),
                TimeOut = new DateTime(date.Year, date.Month, date.Day, timeOut.Hour, timeOut.Minute, 0).ToUniversalTime(),
                UserId = input.UserId
            };
            _context.Add(ingoing);
            _context.SaveChanges();
            return Redirect($"/users/timeclock/{input.UserId}");
        }
        return Redirect($"/users/timeclock/{input.UserId}");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}