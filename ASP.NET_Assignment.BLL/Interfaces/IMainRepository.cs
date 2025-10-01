using ASP.NET_Assignment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_Assignment.BLL.Interfaces
{
    public interface IMainRepository<T> where T : BaseModel
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
        Task AddAsync(T model);
        Task Update(T model);
        void Delete(T model);
    }
}
