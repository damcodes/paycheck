using System;
using System.ComponentModel.DataAnnotations;

namespace PaycheckBackend.Models.Dto
{
	public class PaycheckDtoCreate
	{
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public int JobId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}

