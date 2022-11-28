using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaycheckBackend.Models
{
    public class Paycheck
    {
        public int PaycheckId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public double Amount { get; set; }
        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User? User { get; set; }
        [Required]
        [ForeignKey(nameof(Job))]
        public int JobId { get; set; }
        public Job Job { get; set; }
    }
}