using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.Services;

namespace WebApplication1.Controllers.CustomFilterAttributes
{
	public class IsTraineeAttribute : ActionFilterAttribute
    {
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			UserService userService = context.HttpContext.RequestServices
				.GetService(typeof(UserService)) as UserService;

			if (!userService.IsTrainee())
			{
				context.Result = new ForbidResult();
			}

			base.OnActionExecuting(context);
		}
	}
}
