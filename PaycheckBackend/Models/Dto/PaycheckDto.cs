using System;
namespace PaycheckBackend.Models.Dto
{
	public class PaycheckDto
	{
		public int PaycheckId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int JobId { get; set; }
	}
}

