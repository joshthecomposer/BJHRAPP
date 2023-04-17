#pragma warning disable CS8602
using BJHRApp.Models;
using System.Diagnostics;
using BJHRApp.Utilities;
using BJHRApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace BJHRApp.Controllers;

[SessionCheck]
public class ScheduleController : Controller
{
    // Instantiate the Logger
    private readonly ILogger<ScheduleController> _logger;
    // Instantiate the DBContext
    private DBContext _context;
    // Inject the Logger and DBContext via the constructor
    public ScheduleController(ILogger<ScheduleController> logger, DBContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("/schedules/create")]
    public IActionResult Schedules()
    {
        int? UserId;
        UserId = HttpContext.Session.GetInt32("UserId");
        if(ModelState.IsValid)
        {
            Console.WriteLine("yay it's valid");
            return Redirect($"/users/timeclock/{UserId}");
        }
        else
        {
            return View("TimeClockDashboard");
        }
        
    }
}