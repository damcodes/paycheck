using System.ComponentModel.DataAnnotations;

namespace PaycheckBackend.Models.Dto
{
    public class UserDtoAuthenticateLogin
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = "";
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = "";
    }
}