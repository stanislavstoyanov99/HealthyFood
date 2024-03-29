﻿// ReSharper disable VirtualMemberCallInConstructor
namespace HealthyFood.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HealthyFood.Data.Common.Models;
    using HealthyFood.Data.Models.Enumerations;
    using Microsoft.AspNetCore.Identity;

    using static HealthyFood.Data.Common.DataValidation;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();

            this.Articles = new HashSet<Article>();
            this.ArticleComments = new HashSet<ArticleComment>();
            this.Recipes = new HashSet<Recipe>();
            this.Reviews = new HashSet<Review>();
            this.ReviewComments = new HashSet<ReviewComment>();
        }

        [Required]
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public DateTime? ChangedPasswordOn { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        public virtual ICollection<ArticleComment> ArticleComments { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<ReviewComment> ReviewComments { get; set; }
    }
}
