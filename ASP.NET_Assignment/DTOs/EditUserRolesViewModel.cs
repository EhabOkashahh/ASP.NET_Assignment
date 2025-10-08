namespace ASP.NET.Assignment.PL.DTOs
{
    public class EditUserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public List<UserRolesDto> Roles { get; set; } = new();
    }
}
