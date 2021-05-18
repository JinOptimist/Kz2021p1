using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Services;

namespace WebApplication1.Controllers.CustomFilterAttributes
{
    public class IsTvCastingDirectorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userService = context.HttpContext
                .RequestServices.GetService(typeof(IUserService)) as IUserService;

            if (!userService.IsTvCastingDirector())
            {
                context.Result = new ForbidResult();
            }
            base.OnActionExecuting(context);
        }
    }
}
