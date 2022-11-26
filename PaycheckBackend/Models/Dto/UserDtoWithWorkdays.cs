using System;

namespace PaycheckBackend.Models.Dto
{
    public class UserDtoWithWorkdays
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public IEnumerable<WorkdayDto>? Workdays { get; set; }
    }
}

