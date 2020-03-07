using System.ComponentModel.DataAnnotations;

namespace ForumQuestions.Models
{
    public class LoginViewModel
    {
        [Required]
        [UIHint("email")]
        public string Email { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }
}
