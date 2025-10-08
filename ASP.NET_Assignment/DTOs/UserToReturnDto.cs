using Microsoft.AspNetCore.Identity;
using System.Data.Common;

namespace ASP.NET.Assignment.PL.DTOs
{
    public class UserToReturnDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<UserRolesDto>? Roles { get; set; } = new List<UserRolesDto>();

    }
}
