using System.ComponentModel.DataAnnotations;

namespace ASP.NET.Assignment.PL.DTOs
{
    public class CreateEmployeeDTO
    {
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        public int? Age { get; set; }

        [Required(ErrorMessage = "Salary is required!")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone number is required!")]
        public long Phone { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Active status is required!")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Deleted status is required!")]
        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "Hire date is required!")]
        public DateOnly HireDate { get; set; }

        [Required(ErrorMessage = "Date of creation is required!")]
        public DateTime DateOfCreation { get; set; }

        public  int? DepartmentId { get; set; }

        public string? ImageName { get; set; }

        public IFormFile? Image { get; set; }

    }
}
