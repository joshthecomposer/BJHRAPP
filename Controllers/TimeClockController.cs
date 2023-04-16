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
        List<Punch> punches = _context.Punches.Where(p => p.UserId == UserId).OrderBy(p=>p.Time).ToList();
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
        Punch? latestPunch = _context.Punches.Where(p=>p.UserId == UserId).OrderBy(p=>p.Time).LastOrDefault();
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
        List<Punch>? punches = _context.Punches.Where(p => p.UserId == UserId && p.Time.Date == DateTime.Now.Date).ToList();
        if (!punches.Any()) { return true; }
        return (DateTime.Now.ToLocalTime() > punches[punches.Count - 1].Time.ToLocalTime().AddMinutes(1) ? true : false);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}