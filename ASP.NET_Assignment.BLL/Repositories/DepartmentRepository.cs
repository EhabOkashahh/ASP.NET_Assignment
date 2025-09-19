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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AssignmentDbContext _context;

        public DepartmentRepository(AssignmentDbContext assignmentDbContext)
        {
            _context = assignmentDbContext;
        }

        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList();
        }
        public Department? Get(int id)
        {

            return _context.Departments.Find(id);
        }

        public int Add(Department model)
        {
            _context.Departments.Add(model);
            return _context.SaveChanges();
        }
        public int Update(Department model)
        {
            _context.Departments.Update(model);
            return _context.SaveChanges();
        }
        public int Delete(Department model)
        {
            _context.Departments.Remove(model);
            return _context.SaveChanges();
        }


    }
}
