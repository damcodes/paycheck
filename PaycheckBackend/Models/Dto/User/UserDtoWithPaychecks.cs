using System;

namespace PaycheckBackend.Models.Dto
{
	public class UserDtoWithPaychecks
	{
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public IEnumerable<PaycheckDtoUserWithPaychecks>? Paychecks { get; set; }
    }
}

