﻿namespace HealthyFood.Models.ViewModels.AdminDashboard
{
    using HealthyFood.Data.Models;
    using System.Collections.Generic;

    public class DashboardContentModel
    {
        public int RegisteredUsers { get; set; }

        public int UsersToday { get; set; }

        public int Admins { get; set; }

        public int RecipesCount {get;set;}

        public int ArticlesCount { get; set; }

        public int ReviewsCount { get; set; }

        public int CategoriesCount { get; set; }

        public List<Category> TopCategories { get; set; }

        public List<ApplicationUser> TopUsers { get; set; }
    }
}