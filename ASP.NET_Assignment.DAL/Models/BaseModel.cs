using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_Assignment.DAL.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Name { get; set; }

    }
}
