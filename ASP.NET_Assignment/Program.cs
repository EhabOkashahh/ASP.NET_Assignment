using ASP.NET.Assignment.PL.Helpers;
using ASP.NET.Assignment.PL.Helpers.Services;
using ASP.NET.Assignment.PL.Mapper;
using ASP.NET_Assignment.BLL.Interfaces;
using ASP.NET_Assignment.BLL.Repositories;
using ASP.NET_Assignment.DAL.Data.Contexts;
using ASP.NET_Assignment.DAL.Models;
using MailKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Assignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IDepartmentRepository , DepartmentRepository>(); // Allow DI for DepartmentRepository 
            builder.Services.AddScoped<IEmployeeRepositroy, EmployeeRepository>(); // Allow DI for DepartmentRepository 
            builder.Services.AddDbContext<AssignmentDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))); // Allow DI for AssignmentDbContext
            builder.Services.AddAutoMapper(M=>M.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new DepartmentProfile()));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddIdentity<AppUser , AppRole>().AddEntityFrameworkStores<AssignmentDbContext>().AddDefaultTokenProviders();
            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";
            });
            builder.Services.AddScoped<RoleService>();
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
            builder.Services.AddScoped<IMailServices, MailServices>();
            //builder.Services.AddScoped    => Create Object Life Time Per Request   then will be unreachable object
            //builder.Services.AddTransient => Create Object Life Time Per Operation   then will be unreachable object
            //builder.Services.AddSingleton => Create Object Life Time Per Applecation   then will be unreachable object


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
