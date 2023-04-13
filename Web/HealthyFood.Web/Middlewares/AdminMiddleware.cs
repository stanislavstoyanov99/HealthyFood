namespace HealthyFood.Web.Middlewares
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthyFood.Common;
    using HealthyFood.Data.Models;
    using HealthyFood.Data.Models.Enumerations;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    public class AdminMiddleware
    {
        private readonly RequestDelegate next;

        public AdminMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            await this.SeedUserInRoles(userManager);
            await this.next(context);
        }

        private async Task SeedUserInRoles(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any(x => x.UserName == GlobalConstants.AdministratorUsername))
            {
                var user = new ApplicationUser
                {
                    UserName = GlobalConstants.AdministratorUsername,
                    Email = GlobalConstants.AdministratorEmail,
                    FullName = GlobalConstants.AdministratorFullName,
                    Gender = Gender.Male,
                };

                var result = await userManager.CreateAsync(user, GlobalConstants.AdministratorPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
                }
                else
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
