#pragma warning disable CS8602
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BJHRApp.Models;
using BJHRApp.Data;

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

    [SessionCheck]
    [HttpGet("/")]
    public IActionResult Index()
    {   
        int? userId = HttpContext.Session.GetInt32("UserId");
        User? currentLoggedUser = _context.Users.FirstOrDefault(u => u.UserId == userId);
        ViewBag.FirstName = currentLoggedUser.FirstName;
        ViewBag.LastName = currentLoggedUser.LastName;
        return View();
    }

    [HttpGet("/test")]
    public IActionResult PlayGround()
    {
        return View("Test");
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
