using ASP.NET_Assignment.DAL.Data.Configurations;
using ASP.NET_Assignment.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_Assignment.DAL.Data.Contexts
{
    public class AssignmentDbContext : DbContext
    {
        public AssignmentDbContext() : base()
        {
            
        }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = . ; Database = AssignmentAsp; Trusted_Connetion = True; TrustServerCertificate = True");
        }
    }
}
