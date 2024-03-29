﻿namespace HealthyFood.Web.Areas.Identity.Pages.Account.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HealthyFood.Data.Common;
    using HealthyFood.Data.Models.Enumerations;

    public class RegisterInputModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [MaxLength(DataValidation.FullNameMaxLength, ErrorMessage = "The {0} must be max {1} characters long.")]
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string SelectedGender { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<string> Genders { get; set; } = new[]
        {
            nameof(Gender.Male),
            nameof(Gender.Female),
        };
    }
}
