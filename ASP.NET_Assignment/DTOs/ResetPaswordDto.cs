using System.ComponentModel.DataAnnotations;

namespace ASP.NET.Assignment.PL.DTOs
{
    public class ResetPaswordDto
    {
        [Required(ErrorMessage = "Password is Requir Field")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password is Requir Field")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Confirm password doees not match the password")]
        public string ConfirmPassword { get; set; }
    }
}
