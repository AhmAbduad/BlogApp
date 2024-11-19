using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class RegisterDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}