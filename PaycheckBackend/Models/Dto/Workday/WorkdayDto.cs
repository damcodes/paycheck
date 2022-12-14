using System;
namespace PaycheckBackend.Models.Dto
{
	public class WorkdayDto
	{
		public int WorkdayId { get; set; }
		public DateTime TimeIn { get; set; }
		public DateTime TimeOut { get; set; }
		public double? Tips { get; set; }
		public double WagesEarned { get; set; }
		public int UserId { get; set; }
		public int JobId { get; set; }
		public int PaycheckId { get; set; }
	}
}

