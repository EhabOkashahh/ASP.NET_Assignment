using System.ComponentModel.DataAnnotations;

namespace ASP.NET.Assignment.PL.DTOs
{
    public class ForgetPasswordDto
    {
        [Required(ErrorMessage = "Email is Requir Field")]
        public string Email { get; set; }
    }
}
