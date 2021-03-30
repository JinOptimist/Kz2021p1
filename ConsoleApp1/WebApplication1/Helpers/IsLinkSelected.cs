using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Helpers
{
    public static class IsLinkSelected
    {
        public static string IsSelected(this IHtmlHelper html, string controller = null, string action = null)
        {
            string CssClass = "active";
            string CurrentAction = (string)html.ViewContext.RouteData.Values["action"];
            string CurrentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (string.IsNullOrEmpty(controller))
                controller = CurrentController;

            if (string.IsNullOrEmpty(action))
                action = CurrentAction;

            return controller == CurrentController && action == CurrentAction ?
                CssClass : string.Empty;
        }
    }
}
