using System.ComponentModel.DataAnnotations;

namespace ASP.NET.Assignment.PL.DTOs
{
    public class SignUpDto
    {
        [Required(ErrorMessage = "Username is Requir Field")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "FirstName is Requir Field")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is Requir Field")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is Requir Field")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Requir Field")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password is Requir Field")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "Confirm password doees not match the password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "You Must Agree Our Terms To use Our Services")]
        public bool IsAgree { get; set; }

    }
}
