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
    public class GenericMainClass<T> : IMainRepository<T> where T : BaseModel
    {
        private readonly AssignmentDbContext _context;

        public GenericMainClass(AssignmentDbContext assignmentDbContext)
        {
            _context = assignmentDbContext;
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)_context.Set<Employee>().Include(E => E.Department).ToList();
            }
            return  _context.Set<T>().ToList();
        }
        public T? Get(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return _context.Employees.Include(E=>E.Department).FirstOrDefault(E=>E.Id == id) as T; 
            }
            return _context.Set<T>().Find(id);
        }

        public void Add(T model)
        {
            _context.Set<T>().Add(model);
        }
        public void Update(T model)
        {
            var old = Get(model.Id);
            _context.Entry(old).CurrentValues.SetValues(model);


        }
        public void Delete(T model)
        {
            _context.Set<T>().Remove(model);
        }
    }
}
