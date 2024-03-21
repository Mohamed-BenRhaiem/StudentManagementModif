using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm password")]
        [Compare("Password",ErrorMessage ="Password and confirmation password do not match.")]
        public required string ConfirmPassword { get; set;}
    }
}
