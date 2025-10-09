using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_Assignment.DAL.Models
{
    public class AppRole : IdentityRole
    {
        public int Level { get; set; }  
    }
}
