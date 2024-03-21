using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace StudentManagement.Models.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name ="Role")]
        public string RoleName {  get; set; }
    }
}
