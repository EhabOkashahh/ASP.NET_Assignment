using ASP.NET_Assignment.DAL.Data.Configurations;
using ASP.NET_Assignment.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_Assignment.DAL.Data.Contexts
{
    public class AssignmentDbContext : IdentityDbContext<AppUser>
    {
        public AssignmentDbContext(DbContextOptions<AssignmentDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = . ; Database = AssignmentAsp; Trusted_Connetion = True; TrustServerCertificate = True");
        //}
    }
}
