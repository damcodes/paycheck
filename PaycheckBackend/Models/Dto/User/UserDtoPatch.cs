using System;
using System.ComponentModel.DataAnnotations;

namespace PaycheckBackend.Models.Dto
{
    public class UserDtoPatch
    {
        //[Required(ErrorMessage = "First name is required")]
        public string? FirstName { get; set; }
        //[Required(ErrorMessage = "Last name is required")]
        public string? LastName { get; set; }
        //[Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Email address is not valid")]
        public string? Email { get; set; }
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be greater than 6 characters and less than 20 characters")]
        public string? Password { get; set; }
    }
}

