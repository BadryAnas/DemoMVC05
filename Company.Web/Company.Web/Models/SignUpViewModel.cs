using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [EmailAddress(ErrorMessage = "Invalid Format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmed Password is required")]
        [Compare(nameof(Password) , ErrorMessage ="doesn't match the password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "IsAgree is required")]

        public bool IsAgree { get; set; }

    }
}
