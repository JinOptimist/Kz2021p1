using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Services;

namespace WebApplication1.Controllers.CustomFilterAttributes
{
    public class IsAuthenticatedCitizenAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userServirce = context.HttpContext
                .RequestServices.GetService(typeof(UserService)) as UserService;

            if (userServirce.GetUser() == null) context.Result = new ForbidResult();

            base.OnActionExecuting(context);
        }
    }
}
