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
            return _context.Set<T>().ToList();
        }
        public T? Get(int id)
        {

            return _context.Set<T>().Find(id);
        }

        public int Add(T model)
        {
            _context.Set<T>().Add(model);
            return _context.SaveChanges();
        }
        public int Update(T model)
        {
            var old = Get(model.Id);
            _context.Entry(old).CurrentValues.SetValues(model);

            if (!_context.Entry(old).State.HasFlag(EntityState.Modified))
            {
                return 0;
            }

            return _context.SaveChanges();


        }
        public int Delete(T model)
        {
            _context.Set<T>().Remove(model);
            return _context.SaveChanges();
        }
    }
}
