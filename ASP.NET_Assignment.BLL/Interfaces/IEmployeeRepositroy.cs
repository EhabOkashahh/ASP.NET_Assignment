using ASP.NET_Assignment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_Assignment.BLL.Interfaces
{
    public interface IEmployeeRepositroy : IMainRepository<Employee>
    {
        IEnumerable<Employee> GetByName(string Text);
    }
}
