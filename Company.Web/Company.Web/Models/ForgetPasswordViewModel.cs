using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "First Name is required")]
        [EmailAddress(ErrorMessage = "Invalid Format")]
        public string Email { get; set; }
    }
}
