using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Localization;
using WebApplication1.Services;

namespace WebApplication1.Controllers.CustomFilterAttributes
{
    public class LocalizedAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            var userServirce = context.HttpContext
                .RequestServices.GetService(typeof(UserService)) as UserService;

            CultureInfo culture = null;
            switch (userServirce.GetUser()?.Local)
            {
                case EfStuff.Model.Local.Rus:
                    culture = CultureInfo.GetCultureInfo("ru-Ru");
                    break;
                case EfStuff.Model.Local.Eng:
                    culture = CultureInfo.GetCultureInfo("en-US");
                    break;
            }

            if (culture != null)
            {
                CultureInfo.DefaultThreadCurrentCulture = culture;
                Resource.Culture = culture;
            }

            base.OnResultExecuted(context);
        }
    }
}
