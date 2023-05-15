using OpenAI_API;

namespace HealthyFood.Web
{
    using System.Net;
    using System.Reflection;

    using CloudinaryDotNet;
    using HealthyFood.Common.Attributes;
    using HealthyFood.Data;
    using HealthyFood.Data.Common.Repositories;
    using HealthyFood.Data.Models;
    using HealthyFood.Data.Repositories;
    using HealthyFood.Data.Seeding;
    using HealthyFood.Models.ViewModels;
    using HealthyFood.Services.Data;
    using HealthyFood.Services.Data.Interfaces;
    using HealthyFood.Services.Mapping;
    using HealthyFood.Services.Messaging;
    using HealthyFood.Web.Middlewares;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services, builder.Configuration, builder.Environment.IsDevelopment());
            var app = builder.Build();
            Configure(app);
            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration, bool isDevelopment)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(isDevelopment ? configuration.GetConnectionString("DevelopmentConnection") :
                    configuration.GetConnectionString("ProductionConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services.AddControllersWithViews(
                options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                }).AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddScoped<PasswordExpirationCheckAttribute>();

            // Application services
            services.AddTransient<IEmailSender>(
                serviceProvider => new SendGridEmailSender(configuration["SendGridHealthyFood:ApiKey"]));
            services.AddTransient<IRecipesService, RecipesService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<IContactsService, ContactsService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IArticlesService, ArticlesService>();
            services.AddTransient<IArticleCommentsSertvice, ArticleCommentsService>();
            services.AddTransient<IFaqService, FaqService>();
            services.AddTransient<IApplicationUsersService, ApplicationUsersService>();
            services.AddTransient<IPrivacyService, PrivacyService>();
            services.AddTransient<IReviewsService, ReviewsService>();

            services.AddTransient(_ => new OpenAIAPI(new APIAuthentication("YOUR_API_KEY", "org-yourOrgHere")));

            // External login providers
            services.AddAuthentication()
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = configuration["Authentication:Facebook:AppSecret"];
                });

            var account = new Account(
               configuration["Cloudinary:AppName"],
               configuration["Cloudinary:AppKey"],
               configuration["Cloudinary:AppSecret"]);

            Cloudinary cloudinary = new Cloudinary(account);

            services.AddSingleton(cloudinary);
        }

        private static void Configure(WebApplication app)
        {
            // Seed data on application startup
            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStatusCodePagesWithRedirects("/Home/HttpError?statusCode={0}");
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAdminMiddleware();

            app.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute("subscription", "{controller=Home}/{action=ThankYouSubscription}/{email?}");
            app.MapRazorPages();
        }
    }
}
