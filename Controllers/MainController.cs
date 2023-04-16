#pragma warning disable CS8602
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BJHRApp.Models;
using BJHRApp.Data;
using BJHRApp.Utilities;

namespace BJHRApp.Controllers;

[SessionCheck]
public class MainController : Controller
{
    private readonly ILogger<MainController> _logger;
    private DBContext _context;
    public MainController(ILogger<MainController> logger, DBContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("/")]
    public IActionResult Index()
    {
        User? currentLoggedUser = _context.Users.FirstOrDefault(u => u.Id == HttpContext.Session.GetInt32("UserId"));
        ViewBag.FirstName = currentLoggedUser.FirstName;
        ViewBag.LastName = currentLoggedUser.LastName;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
