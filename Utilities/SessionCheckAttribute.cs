using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BJHRApp.Utilities;
public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        int? userId = filterContext.HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            filterContext.Result = new RedirectResult("/users/login");
        }
    }
}