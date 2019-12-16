using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using EmployeeManagement.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup (IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 10;
                    options.Password.RequiredUniqueChars = 2;
                    options.Password.RequireNonAlphanumeric = false;
                })
                    .AddEntityFrameworkStores<AppDbContext>();

            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequiredLength = 10;
            //    options.Password.RequiredUniqueChars = 2;
            //    options.Password.RequireNonAlphanumeric = false;
            //});
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "92991678884-69705q3r4s4cc6uguhqa5ek221k8do8h.apps.googleusercontent.com";
                options.ClientSecret = "KxpnNEeAtvSBnrr3KyeWntxf";
            }).AddFacebook(options =>
            {
                options.AppId = "710079959484336";
                options.AppSecret = "901dcb3b3b28217a6e76e7669d2316bc";
            }); 

            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy",
                    policy=>policy.RequireClaim("Delete Role").RequireClaim("Create Role"));

                //options.AddPolicy("EditRolePolicy",
                //    policy => policy.RequireClaim("Edit Role","true"));

                //options.AddPolicy("EditRolePolicy",
                //    policy => policy.RequireAssertion(context =>

                //            context.User.IsInRole("Admin") &&
                //            context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true") ||
                //            context.User.IsInRole("Super Admin")
                //    ));

                options.AddPolicy("EditRolePolicy", policy =>
                    policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));


            });


            services.AddSingleton<IAuthorizationHandler,
                CanEditOnlyOtherAdminRolesAndClaimsHandler>();
            services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();
            services.ConfigureApplicationCookie(options =>
                {
                    options.AccessDeniedPath = new PathString("/Administration/AccessDenied");
                }
            );

            services.AddMvc(config =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                    config.Filters.Add(new AuthorizeFilter(policy));
                })
                    .AddXmlSerializerFormatters();
            //services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
            services.AddScoped<IEmployeeRepository, SqlEmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions
                {
                    SourceCodeLineCount=1
                };
                
                app.UseDeveloperExceptionPage(developerExceptionPageOptions);
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("foo.html");
            //app.UseFileServer(fileServerOptions);


            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();
            app.UseAuthentication();
            app.UseMvc(routes => {
                routes.MapRoute("default","{controller=Home}/{action=Index}/{id?}");
            });

            //app.Run(async (context) =>
            //{
            //    //throw new Exception("Some exception");
            //    await context.Response.WriteAsync("Hello World");
                
            //});
        }
    }
}
