using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class LoginDto
    {
        
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
