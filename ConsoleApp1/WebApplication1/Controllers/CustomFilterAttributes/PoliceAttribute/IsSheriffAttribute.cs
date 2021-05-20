using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.Services;

namespace WebApplication1.Controllers.CustomFilterAttributes
{
	public class IsSheriffAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            IUserService userServirce = context.HttpContext
                .RequestServices.GetService(typeof(IUserService)) as IUserService;

            if (!userServirce.IsSheriff())
            {
                context.Result = new ForbidResult();
            }

            base.OnActionExecuting(context);
        }
    }
}
