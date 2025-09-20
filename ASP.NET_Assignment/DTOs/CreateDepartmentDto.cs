using System.ComponentModel.DataAnnotations;

namespace ASP.NET.Assignment.PL.DTOs
{
    public class CreateDepartmentDto
    {
        [Required(ErrorMessage = "Code Is Required!")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name Is Required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Date Of Creation Is Required!")]
        public DateTime DateOfCreation { get; set; }
    }
}
