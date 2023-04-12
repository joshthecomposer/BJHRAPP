using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BJHRApp.Models;
using BJHRApp.Data;

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

    [HttpGet("{UserId}")]
    public ViewResult TimeClockDashboard(int id)
    {
        return View();
    }

    [HttpPost("{UserId}/punch")]
    public RedirectResult CreatePunch(int id)
    {
        return Redirect("/users/timeclock/"+ HttpContext.Session.GetInt32("UserId"));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}