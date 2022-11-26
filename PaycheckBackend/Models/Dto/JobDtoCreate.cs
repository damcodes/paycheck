using System;
using System.ComponentModel.DataAnnotations;

namespace PaycheckBackend.Models.Dto
{
	public class JobDtoCreate
	{
		[Required]
		[StringLength(60, ErrorMessage = "Company name can't be longer than 60 characters")]
		public string? Company { get; set; }
		[Required]
		public double PayRate { get; set; }
		[Required]
		public TimeSpan PayPeriodLength { get; set; }
		[Required]
		public int UserId { get; set; }
	}
}

