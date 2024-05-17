using System.ComponentModel.DataAnnotations;

namespace WebApplication1.viewModel
{
    public class EditUserProfileViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Current password")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Display(Name = "New password")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }
    }
}
