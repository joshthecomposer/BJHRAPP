using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BJHRApp.Models;

namespace BJHRApp.Controllers;

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
        HttpContext.Session.SetInt32("UserId", 1);  //TODO: Get rid of this later
        if (HttpContext.Session.GetInt32("UserId") == null)
        {
            return Redirect("/register");
        }
        return View("Index");
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

    public RedirectResult Create(User u)
    {
        if (ModelState.IsValid)
        {
            _context.Add(u);
            _context.SaveChanges();
        }
        return Redirect("/");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
