using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BJHRApp.Utilities;
public class ClaimCheck : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        string idString = (string)filterContext.RouteData.Values["UserId"]!;
        int inputId = int.Parse(idString);

        if (filterContext.HttpContext.Session.GetInt32("UserId") != inputId)
        {
            filterContext.Result = new RedirectResult("/");
        }
    }
}