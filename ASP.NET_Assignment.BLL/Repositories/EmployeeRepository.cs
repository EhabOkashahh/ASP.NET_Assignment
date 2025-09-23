using ASP.NET_Assignment.BLL.Interfaces;
using ASP.NET_Assignment.DAL.Data.Contexts;
using ASP.NET_Assignment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_Assignment.BLL.Repositories
{
    public class EmployeeRepository : GenericMainClass<Employee> , IEmployeeRepositroy
    {
        public EmployeeRepository(AssignmentDbContext assignmentDbContext) : base(assignmentDbContext)
        {
            
        }

    }
}
