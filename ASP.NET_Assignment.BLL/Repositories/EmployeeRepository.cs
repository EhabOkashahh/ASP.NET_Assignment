using ASP.NET_Assignment.BLL.Interfaces;
using ASP.NET_Assignment.DAL.Data.Contexts;
using ASP.NET_Assignment.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_Assignment.BLL.Repositories
{
    public class EmployeeRepository : GenericMainClass<Employee> , IEmployeeRepositroy
    {
        private readonly AssignmentDbContext _Context;

        public EmployeeRepository(AssignmentDbContext assignmentDbContext) : base(assignmentDbContext)
        {
            _Context = assignmentDbContext;
        }

        public async Task<IEnumerable<Employee>> GetByNameAsync(string Text) => await _Context.Employees.Where(E=>E.Name.ToLower().Contains(Text.ToLower())).ToListAsync();
    }
}
