namespace HealthyFood.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthyFood.Common;
    using HealthyFood.Data.Models;
    using HealthyFood.Models.ViewModels.ApplicationUsers;
    using HealthyFood.Services.Data.Interfaces;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ApplicationUsersController : AdministrationController
    {
        private readonly UserManager<ApplicationUser> applicationUserManager;
        private readonly IApplicationUsersService applicationUsersService;
        private readonly RoleManager<ApplicationRole> roleManager;

        public ApplicationUsersController(
            UserManager<ApplicationUser> applicationUserManager,
            IApplicationUsersService applicationUsersService,
            RoleManager<ApplicationRole> roleManager)
        {
            this.applicationUserManager = applicationUserManager;
            this.applicationUsersService = applicationUsersService;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> GetAll()
        {
            var users = await this.applicationUsersService
                .GetAllApplicationUsersAsync<ApplicationUserDetailsViewModel>();

            return this.View(users);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await this.applicationUserManager.FindByIdAsync(id);
            var isAdmin = await this.applicationUserManager.IsInRoleAsync(user, GlobalConstants.AdministratorRoleName);
            var isUser = await this.applicationUserManager.IsInRoleAsync(user, GlobalConstants.UserRoleName);

            var currUserRole = user.Roles.FirstOrDefault(x => x.UserId == id);
            var currUserRoleName = await this.roleManager.FindByIdAsync(currUserRole.RoleId);

            var applicationUserEditViewModel = new ApplicationUserEditViewModel
            {
                RoleId = currUserRole.RoleId,
                RoleName = currUserRoleName.Name,
            };

            applicationUserEditViewModel.RolesList = this.roleManager.Roles
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                })
                .ToList();

            if (currUserRoleName.Name == GlobalConstants.AdministratorRoleName && isAdmin)
            {
                applicationUserEditViewModel.RolesList
                    .Find(x => x.Text == GlobalConstants.AdministratorRoleName).Selected = true;
            }

            if (currUserRoleName.Name == GlobalConstants.UserRoleName && isUser)
            {
                applicationUserEditViewModel.RolesList
                    .Find(x => x.Text == GlobalConstants.UserRoleName).Selected = true;
            }

            return this.View(applicationUserEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUserEditViewModel model, string id)
        {
            if (!this.ModelState.IsValid)
            {
                model.RolesList = this.roleManager.Roles
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                })
                .ToList();

                return this.View(model);
            }

            if (model.NewRole == model.RoleName)
            {
                model.RolesList = this.roleManager.Roles
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                })
                .ToList();

                return this.View(model);
            }

            var user = await this.applicationUserManager.FindByIdAsync(id);

            await this.applicationUserManager.RemoveFromRoleAsync(user, model.RoleName);

            await this.applicationUserManager.AddToRoleAsync(
                user,
                model.NewRole);

            return this.RedirectToAction("GetAll", "ApplicationUsers", new { area = "Administration" });
        }

        public async Task<IActionResult> Ban(string id)
        {
            var applicationUserToBan = await this.applicationUsersService
                .GetViewModelByIdAsync<ApplicationUserDetailsViewModel>(id);

            return this.View(applicationUserToBan);
        }

        [HttpPost]
        public async Task<IActionResult> Ban(ApplicationUserDetailsViewModel applicationUserDetailsViewModel)
        {
            await this.applicationUsersService.BanByIdAsync(applicationUserDetailsViewModel.Id);

            return this.RedirectToAction("GetAll", "ApplicationUsers", new { area = "Administration" });
        }

        public async Task<IActionResult> Unban(string id)
        {
            var applicationUserToUnban = await this.applicationUsersService
                .GetViewModelByIdAsync<ApplicationUserDetailsViewModel>(id);

            return this.View(applicationUserToUnban);
        }

        [HttpPost]
        public async Task<IActionResult> Unban(ApplicationUserDetailsViewModel applicationUserDetailsViewModel)
        {
            await this.applicationUsersService.UnbanByIdAsync(applicationUserDetailsViewModel.Id);

            return this.RedirectToAction("GetAll", "ApplicationUsers", new { area = "Administration" });
        }
    }
}
