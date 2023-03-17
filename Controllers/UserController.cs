using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BJHRApp.Models;

namespace BJHRApp.Controllers;
[Route("users")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private DBContext _context;
    public UserController(ILogger<UserController> logger, DBContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("register")]
    public ViewResult Register()
    {
        return View();
    }

    [HttpGet("login")]
    public ViewResult Login()
    {
        return View();
    }

    [HttpGet("logout")]
    public RedirectToActionResult Logout()
    {
        // HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    [HttpGet("settings/{UserId}")]
    public ViewResult Settings()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}