﻿namespace YovevElectric.Web
{
    using System.Reflection;
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using YovevElectric.Data;
    using YovevElectric.Data.Common;
    using YovevElectric.Data.Common.Repositories;
    using YovevElectric.Data.Models;
    using YovevElectric.Data.Repositories;
    using YovevElectric.Data.Seeding;
    using YovevElectric.Services.Data;
    using YovevElectric.Services.Mapping;
    using YovevElectric.Services.Messaging;
    using YovevElectric.Web.ViewModels;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication().AddFacebook(option =>
            {
                option.AppId = this.configuration["Facebook:AppKey"];
                option.AppSecret = this.configuration["Facebook:AppSecret"];
            }).AddGoogle(option =>
            {
                option.ClientId = this.configuration["Google:AppKey"];
                option.ClientSecret = this.configuration["Google:AppSecret"];
            });

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
                    });
            services.AddRazorPages();

            Account account = new Account(
                                 this.configuration["Cloudinary:AppName"],
                                 this.configuration["Cloudinary:AppKey"],
                                 this.configuration["Cloudinary:AppSecret"]);

            Cloudinary cloudinary = new Cloudinary(account);
            services.AddSingleton(cloudinary);

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, SendGridEmailSender>(provider =>
                new SendGridEmailSender(this.configuration["SendGrid:AppKey"], this.configuration["SendGrid:Email"], this.configuration["SendGrid:Username"]));
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IBagService, BagService>();
            services.AddTransient<IImgService, ImgService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IOrderDataService, OrderDataService>();
            services.AddTransient<IDiscountsService, DiscountsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
