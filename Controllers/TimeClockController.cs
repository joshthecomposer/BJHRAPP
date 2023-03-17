using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BJHRApp.Models;

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
        //TODO:Add logic for creating and insertig a punch for current user into DB
        return Redirect("/users/timeclock/"+ HttpContext.Session.GetInt32("UserId"));
    }

    //TODO:Add a route/View for Admins to add/update user punches manually.
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}