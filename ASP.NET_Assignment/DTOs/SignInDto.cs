using System.ComponentModel.DataAnnotations;

namespace ASP.NET.Assignment.PL.DTOs
{
    public class SignInDto
    {
        [Required(ErrorMessage = "Email is Requir Field")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Requir Field")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
