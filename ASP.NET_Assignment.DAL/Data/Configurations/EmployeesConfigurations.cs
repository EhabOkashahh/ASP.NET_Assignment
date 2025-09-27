using ASP.NET_Assignment.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_Assignment.DAL.Data.Configurations
{
    internal class EmployeesConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Salary).HasColumnType("decimal(18,2)");
            builder.Property(E=>E.Id).UseIdentityColumn(10,10);

            builder.HasOne(E=>E.Department).WithMany(D=>D.Employees).HasForeignKey(E => E.DepartmentId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
