using System;
using System.ComponentModel.DataAnnotations;

namespace PaycheckBackend.Models.Dto
{
	public class UserDtoCreate
	{
		[Required(ErrorMessage = "First name is required")]
		[StringLength(40, ErrorMessage = "First name can't be longer than 40 characters")]
		public string? FirstName { get; set; }
		[Required(ErrorMessage = "Last name is required")]
		[StringLength(40, ErrorMessage = "Last name can't be longer than 40 characters")]
		public string? LastName { get; set; }
		[Required(ErrorMessage = "Email is required")]
		[StringLength(60, ErrorMessage = "Email can't be longer than 60 characters")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string? Email { get; set; }
		[Required(ErrorMessage = "Password is required")]
		[StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be greater than 6 characters and less than 20 characters")]
		public string? Password { get; set; }
	}
}

