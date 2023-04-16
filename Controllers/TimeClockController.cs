using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BJHRApp.Models;
using BJHRApp.Data;
using System.Text.Json;

namespace BJHRApp.Controllers;
[Route("users/timeclock")]
public class TimeClockController : Controller
{
    private readonly ILogger<TimeClockController> _logger;
    private DBContext _context;
    public TimeClockController(ILogger<TimeClockController> logger, DBContext context)
    {
        _logger = logger;
        _context = context;
    }

    [SessionCheck]
    [HttpGet("{UserId}")]
    public IActionResult TimeClockDashboard(int UserId)
    {
        if (HttpContext.Session.GetInt32("UserId") == UserId)
        {
            List<Punch> punches = _context.Punches.Where(p => p.UserId == UserId).ToList();
            if (punches != null)
            {
                ViewBag.Punches = punches;
            }
            return View();
        }
        return Redirect("/logout");
    }

    [SessionCheck]
    [HttpPost("{UserId}/punch")]
    public RedirectResult CreatePunch(int UserId)
    {
        if (HttpContext.Session.GetInt32("UserId") == UserId)
        {
            if (!IsPunchValid(UserId))
            {
                return Redirect("/users/timeclock/"+UserId);
            }
            else
            {
                _context.Add(new Punch { UserId = UserId });
                _context.SaveChanges();
                return Redirect("/users/timeclock/"+ HttpContext.Session.GetInt32("UserId"));
            }
        }
        return Redirect("/users/logout");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private bool IsPunchValid(int UserId)
    {
        List<Punch>? punches = _context.Punches.Where(p => p.UserId == UserId && p.Time.Date == DateTime.Now.Date).ToList();
        if (!punches.Any())
        {
            return true;
        }
        if (DateTime.Now.ToLocalTime() > punches[punches.Count-1].Time.AddMinutes(30))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public class SessionCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Find the session, but ensure to check for null
            int? userId = context.HttpContext.Session.GetInt32("UserId");
            // Check to see if value is null
            if(userId == null)
            {
                // Redirect to login page if there was nothing in session
                context.Result = new RedirectToActionResult("Login", "User", null);
            };
        }
    }
}