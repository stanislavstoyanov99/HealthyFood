namespace HealthyFood.Models.ViewModels.ApplicationUsers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HealthyFood.Services.Mapping;
    using HealthyFood.Data.Models;
    using HealthyFood.Data.Models.Enumerations;

    using static HealthyFood.Models.Common.ModelValidation;
    using static HealthyFood.Models.Common.ModelValidation.ApplicationUserValidation;

    public class ApplicationUserDetailsViewModel : IMapFrom<ApplicationUser>
    {
        [Display(Name = IdDisplayName)]
        public string Id { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        [Display(Name = CreatedOnDisplayName)]
        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public Gender Gender { get; set; }

        public IEnumerable<ApplicationRole> UserRoles { get; set; }
    }
}
