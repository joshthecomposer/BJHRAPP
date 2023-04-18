#pragma warning disable CS8602
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BJHRApp.Models;
using BJHRApp.Utilities;
using BJHRApp.Data;

namespace BJHRApp.Controllers;

[SessionCheck]
[Route("users/schedule")]
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
    [ClaimCheck]
    [HttpGet("{UserId}")]
    public IActionResult ScheduleDashboard(int UserId)
    {
        List<User> users = _context.Users.Where(u => u.Id >=0).ToList();
        ViewBag.Users = users;
        return View();
    }

    [HttpPost("/schedules/create")]
    public IActionResult Create()
    {
        int? UserId;
        UserId = HttpContext.Session.GetInt32("UserId");
        if(ModelState.IsValid)
        {
            Console.WriteLine("yay it's valid");

            return Redirect($"/users/schedule/{UserId}");
        }
        else
        {
            return View("ScheduleDashboard");
        }
        
    }
}