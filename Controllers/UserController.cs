using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BJHRApp.Models;

namespace BJHRApp.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private DBContext _context;
    public UserController(ILogger<UserController> logger, DBContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("/register")]
    public ViewResult Register()
    {
        return View("Register");
    }

    [HttpGet("/login")]
    public IActionResult Login()
    {
        return View("Login");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}