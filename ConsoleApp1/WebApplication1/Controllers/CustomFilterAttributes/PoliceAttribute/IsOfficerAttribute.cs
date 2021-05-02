using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.Services;

namespace WebApplication1.Controllers.CustomFilterAttributes
{
	public class IsOfficerAttribute : ActionFilterAttribute
    {
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			IUserService userService = context.HttpContext
				.RequestServices.GetService(typeof(IUserService)) as IUserService;

			if (!userService.IsOfficer())
			{
				context.Result = new ForbidResult();
			}

			base.OnActionExecuting(context);
		}
	}
}
