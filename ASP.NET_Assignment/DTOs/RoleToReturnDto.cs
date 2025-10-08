namespace ASP.NET.Assignment.PL.DTOs
{
    public class RoleToReturnDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserRolesDto> UserRoles { get; set; } = new List<UserRolesDto>();
        public RoleToReturnDto()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
