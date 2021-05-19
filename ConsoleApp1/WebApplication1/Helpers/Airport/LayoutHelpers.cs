using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Airport;

namespace WebApplication1.Helpers
{
    public static class LayoutHelpers
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

        public static string GetColorClassByFlightStatus(this IHtmlHelper html, FlightStatus flightStatus)
        {
            return flightStatus switch
            {
                FlightStatus.Landed => "table-success",
                FlightStatus.Delayed => "table-warning",
                _ => string.Empty
            };
        }
    }
}
