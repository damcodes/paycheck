using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaycheckBackend.Models
{
    public class Workday
    {
        public int WorkdayId { get; set; }
        [Required]
        public DateTime TimeIn { get; set; }
        [Required]
        public DateTime TimeOut { get; set; }
        public double? Tips { get; set; }
        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User? User { get; set; }
        [Required]
        [ForeignKey(nameof(Paycheck))]
        public int PaycheckId { get; set; }
        public Paycheck? Paycheck { get; set; }
        [Required]
        [ForeignKey(nameof(Job))]
        public int JobId { get; set; }
        public Job? Job { get; set; }
    }
}