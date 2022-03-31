using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class SignInUserModel
    {
        [Required(ErrorMessage = "Please enter your email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Please enter valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }
    }
}
