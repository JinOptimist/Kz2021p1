using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Services;

namespace WebApplication1.Controllers.CustomFilterAttributes
{
    public class IsHCWorkerAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userService = context.HttpContext
                .RequestServices.GetService(typeof(UserService)) as UserService;

            if (!userService.IsHCWorker())
            {
                context.Result = new ForbidResult();
            }

            base.OnActionExecuting(context);
        }
    }
}
