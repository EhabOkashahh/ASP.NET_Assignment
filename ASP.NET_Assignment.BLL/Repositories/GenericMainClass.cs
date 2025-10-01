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

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _context.Set<Employee>().Include(E => E.Department).ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return await _context.Employees.Include(E=>E.Department).FirstOrDefaultAsync(E=>E.Id == id) as T; 
            }
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T model)
        {
           await _context.AddAsync(model);
        }

        public async Task Update(T model)
        {
            var old = await GetAsync(model.Id);
            _context.Entry(old).CurrentValues.SetValues(model);


        }
        public void Delete(T model)
        {
            _context.Set<T>().Remove(model);
        }
    }
}
