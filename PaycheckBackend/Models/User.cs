using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaycheckBackend.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be greater than 6 characters and less than 20 characters")]
        [MaxLength(20, ErrorMessage = "Password cannot be longer than 20 characters")]
        public string Password { get; set; }
        public List<Workday>? Workdays { get; set; }
        public List<Paycheck>? Paychecks { get; set; }
        public List<Job>? Jobs { get; set; }
    }
}