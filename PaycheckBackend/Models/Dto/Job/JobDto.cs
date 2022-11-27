using System;
namespace PaycheckBackend.Models.Dto
{
	public class JobDto
	{
		public int JobId { get; set; }
		public string? Company { get; set; }
		public double PayRate { get; set; }
		public TimeSpan PayPeriodLength { get; set; }
		public int UserId { get; set; }
	}
}

