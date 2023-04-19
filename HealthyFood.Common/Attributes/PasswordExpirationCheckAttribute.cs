namespace HealthyFood.Common.Attributes
{
    using System;
    using System.Threading.Tasks;

    using HealthyFood.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class PasswordExpirationCheckAttribute : ActionFilterAttribute
    {
        private const int MaxPasswordAgeInDay = 30;

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var user = context.HttpContext.User;

            if (user != null && user.Identity.IsAuthenticated)
            {
                var userManager = (UserManager<ApplicationUser>)context.HttpContext.RequestServices
                    .GetService(typeof(UserManager<ApplicationUser>));
                var currUser = await userManager.GetUserAsync(user);
                var passwordExpirationDate = currUser.ChangedPasswordOn;
                var infoMessage = $"Your account password expired and should be changed on every {MaxPasswordAgeInDay} days.";

                if (passwordExpirationDate != null)
                {
                    var timeSpan = DateTime.Today - passwordExpirationDate;

                    if (timeSpan.Value.Days >= MaxPasswordAgeInDay && !user.IsInRole(GlobalConstants.AdministratorRoleName))
                    {
                        context.Result = new RedirectToActionResult(
                            "RequestNewPassword", "Home", new ChangePasswordViewModel { Message = infoMessage });
                    }
                }
                else if (!user.IsInRole(GlobalConstants.AdministratorRoleName))
                {
                    context.Result = new RedirectToActionResult(
                            "RequestNewPassword", "Home", new ChangePasswordViewModel { Message = infoMessage });
                }
            }

            await base.OnResultExecutionAsync(context, next);
        }
    }
}
