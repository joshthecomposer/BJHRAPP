#pragma warning disable CS8602
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BJHRApp.Models;
using BJHRApp.Data;
using BJHRApp.Utilities;

namespace BJHRApp.Controllers;
public class ReactController : Controller
{ 

    public ViewResult TestReactView()
    {
        return View();
    }

    public ViewResult CatchRoute()
    {
        return View();
    }
}