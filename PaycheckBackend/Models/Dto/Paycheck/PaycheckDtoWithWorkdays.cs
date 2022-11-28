using System;
namespace PaycheckBackend.Models.Dto
{
	public class PaycheckDtoWithWorkdays
	{
        public int PaycheckId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Amount { get; set; }
        public List<WorkdayDto>? Workdays { get; set; }
        public JobDto? Job { get; set; }
	}
}

