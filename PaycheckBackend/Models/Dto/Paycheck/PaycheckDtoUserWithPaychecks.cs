using System;
namespace PaycheckBackend.Models.Dto
{
	public class PaycheckDtoUserWithPaychecks
	{
        public int PaycheckId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Amount { get; set; }
    }
}

