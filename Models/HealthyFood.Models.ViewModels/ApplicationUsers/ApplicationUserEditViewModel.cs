namespace HealthyFood.Models.ViewModels.ApplicationUsers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.Rendering;

    using static HealthyFood.Models.Common.ModelValidation.ApplicationUserValidation;

    public class ApplicationUserEditViewModel
    {
        public string RoleId { get; set; }

        public string RoleName { get; set; }

        [Required(ErrorMessage = RoleSelectedError)]
        public string NewRole { get; set; }

        public List<SelectListItem> RolesList { get; set; }
    }
}
