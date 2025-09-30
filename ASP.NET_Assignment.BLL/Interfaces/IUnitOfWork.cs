using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_Assignment.BLL.Interfaces
{
    public interface IUnitOfWork
    {
         Lazy<IDepartmentRepository> DepartmentRepository { get;}
         Lazy<IEmployeeRepositroy> EmployeeRepositroy { get;}
        public int ApplyToDB();
    }
}
