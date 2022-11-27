using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaycheckBackend.Models
{
    public class Job
    {
        public int JobId { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public double PayRate { get; set; }
        [Required]
        public TimeSpan PayPeriodLength { get; set; }
        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User? User { get; set; }
        public List<Paycheck>? Paychecks { get; set; }
    }
}