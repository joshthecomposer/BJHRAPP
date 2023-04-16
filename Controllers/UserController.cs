#pragma warning disable CS8602
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using BJHRApp.Models;
using BJHRApp.Data;
using BJHRApp.Utilities;

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

    [HttpPost("create")]
    public IActionResult Create(User newUser)
    {
        if(ModelState.IsValid)
        {
            // Instantiate the password hashyboi
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            // Update the user password to be hashed AF
            newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
            // Add the user to the DB
            _context.Add(newUser);
            // Also add the user ID to session
            _context.SaveChanges();
            HttpContext.Session.SetInt32("UserId", newUser.Id);
            return Redirect("/");
        }
        else
        {
            return View("Register");
        }
    }

    [HttpGet("login")]
    public ViewResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public IActionResult LoginAttempt(LoginUser loginUser)
    {
        if(!ModelState.IsValid)
        {
            return View("Login");
        }
        User? userInDb = _context.Users.FirstOrDefault(u => u.Email == loginUser.Email);
        if (userInDb == null)
        {
            return View("Login");
        }
        PasswordHasher<LoginUser> Hasher = new PasswordHasher<LoginUser>();
        var Result = Hasher.VerifyHashedPassword(loginUser, userInDb.Password, loginUser.Password);
        if(Result == 0)
        {
            ModelState.AddModelError("Password", "Invalid Email/Password");
            return View("Login");
        }
        HttpContext.Session.SetInt32("UserId", userInDb.Id);
        return Redirect("/");
    }

    [HttpGet("logout")]
    public RedirectToActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    [ClaimCheck]
    [HttpGet("settings/{UserId}")]
    public ViewResult Settings(int UserId)
    {
        User? user = _context.Users.FirstOrDefault(u => u.Id == UserId);
        return View(user);
    }

    [ClaimCheck]
    [HttpPost("update/{UserId}")]
    public IActionResult Update(UpdateUser updateUser, int UserId)
    {
        User? OldUser = _context.Users.FirstOrDefault(i => i.Id == UserId);
        if(ModelState.IsValid)
        {
            OldUser.FirstName = updateUser.FirstName;
            OldUser.LastName = updateUser.LastName;
            // _context.Add(OldUser);
            _context.SaveChanges();
            return Redirect("/");
        }
        else
        {
            return View("Settings", OldUser);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}