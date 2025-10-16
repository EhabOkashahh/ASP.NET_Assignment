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
        public string Image { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsConfirmed { get; set; }
        public IFormFile? ImageLink { get; set; } 
        public IEnumerable<string>? Roles { get; set; } = new List<string>();

    }
}
