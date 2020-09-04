using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetProject.Context;
using NetProject.DataAccessor;
using NetProject.DbAccessor;
using NetProject.Util;

namespace NetProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
                options.LoginPath = "/HomePage/PageNotFound";
                options.LogoutPath = "/";
                
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                policy => policy.RequireRole("ADMIN"));
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/";
                options.AccessDeniedPath = "/";
                options.SlidingExpiration = true;
            });
            services.AddDbContext<OurDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

           
            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.IsEssential = true;
            });

            services.AddSingleton<FileData, FileData>();
            services.AddScoped<SliderData, SliderData>();
            services.AddScoped<UserData, UserData>();
            services.AddScoped<TypeProductData, TypeProductData>();
            services.AddScoped<CategoryData, CategoryData>();
            services.AddScoped<ProductData, ProductData>();
            services.AddScoped<BillData, BillData>();
            services.AddScoped<BillDetailData, BillDetailData>();
            services.AddScoped<CommentData, CommentData>();
            services.AddScoped<AddressData, AddressData>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCookiePolicy();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=HomePage}/{action=Index}/{id?}");
            });
        }
    }
}
